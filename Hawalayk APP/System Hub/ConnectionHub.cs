using Hawalayk_APP.Models;

using Microsoft.AspNetCore.SignalR;

namespace Hawalayk_APP.Hubs
{
    public class ConnectionHub : Hub<IConnectionHub>
    {
        private readonly List<ApplicationUser> _Users;
        private readonly List<UserCall> _UserCalls;
        private readonly List<CallOffer> _CallOffers;

        public ConnectionHub(List<ApplicationUser> users, List<UserCall> userCalls, List<CallOffer> callOffers)
        {
            _Users = users;
            _UserCalls = userCalls;
            _CallOffers = callOffers;
        }

        //public async Task Join(string username)
        //{
        //    // Add the new user
        //    _Users.Add(new ApplicationUser
        //    {
        //        Username = username,
        //        ConnectionId = Context.ConnectionId
        //    });

        //    // Send down the new list to all clients
        //    await SendUserListUpdate();
        //}

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Hang up any call/s the user is in
            await HangUp(); // Gets the user from "Context" which is available in the whole hub

            // Remove the user
            _Users.RemoveAll(u => u.ConnectionId == Context.ConnectionId);

            // Send down the new user list to all clients
            await SendUserListUpdate();

            await base.OnDisconnectedAsync(exception);
        }

        public async Task CallUser(ApplicationUser targetConnectionId)
        {
            var callingUser = _Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = _Users.SingleOrDefault(u => u.ConnectionId == targetConnectionId.ConnectionId);

            // Make sure the person we are trying to call is still here
            if (targetUser == null)
            {
                // If not, let the caller know
                await Clients.Caller.CallDeclined(targetConnectionId, "The user you called has left.");
                return;
            }

            // And that they aren't already in a call
            if (GetUserCall(targetUser.ConnectionId) != null)
            {
                await Clients.Caller.CallDeclined(targetConnectionId, string.Format("{0} {1} is already in a call.", targetUser.FirstName, targetUser.LastName));
                return;
            }

            // They are here, so tell them someone wants to talk
            await Clients.Client(targetConnectionId.ConnectionId).IncomingCall(callingUser);

            // Create an offer
            _CallOffers.Add(new CallOffer
            {
                Caller = callingUser,
                Callee = targetUser
            });
        }

        public async Task AnswerCall(bool acceptCall, ApplicationUser callerConnectionId)
        {
            var answeringUser = _Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var callingUser = _Users.SingleOrDefault(u => u.ConnectionId == callerConnectionId.ConnectionId);

            // This can only happen if the server-side came down and clients were cleared, while the user
            // still held their browser session.
            if (answeringUser == null)
            {
                return;
            }

            // Make sure the original caller has not left the page yet
            if (callingUser == null)
            {
                await Clients.Caller.CallEnded(callingUser, "The other user in your call has left.");
                return;
            }

            // Send a decline message if the callee said no
            if (acceptCall == false)
            {
                await Clients.Client(callerConnectionId.ConnectionId).CallDeclined(answeringUser, string.Format("{0} {1} did not accept your call.", answeringUser.FirstName, answeringUser.LastName));
                return;
            }

            //// Make sure there is still an active offer.  If there isn't, then the other use hung up before the Callee answered.
            //var offerCount = _CallOffers.RemoveAll(c => c.Callee.ConnectionId == callingUser.ConnectionId
            //                                      && c.Caller.ConnectionId == targetUser.ConnectionId);


            // Make sure there is still an active offer.  If there isn't, then the other use hung up before the Callee answered.
            var callOffer = _CallOffers.SingleOrDefault(co => co.Caller.ConnectionId == callingUser.ConnectionId && co.Callee.ConnectionId == answeringUser.ConnectionId);

            if (callOffer == null)
            {
                await Clients.Caller.CallEnded(callingUser, string.Format("{0} {1} has already hung up.", callingUser.FirstName, callingUser.LastName));
                return;
            }

            //// And finally... make sure the user hasn't accepted another call already
            //if (GetUserCall(targetUser.ConnectionId) != null)
            //{
            //    // And that they aren't already in a call
            //    await Clients.Caller.CallDeclined(targetConnectionId, string.Format("{0} chose to accept someone elses call instead of yours :(", targetUser.Username));
            //    return;
            //}


            // Ensure callee hasn't accepted another call already and that they aren't already in a call
            var calleeCall = GetUserCall(answeringUser.ConnectionId);
            if (calleeCall != null)
            {
                await Clients.Client(callerConnectionId.ConnectionId).CallDeclined(answeringUser, $"{answeringUser.FirstName} {answeringUser.LastName} chose to accept someone else's call instead of yours :(");
                return;
            }

            // Remove all the other offers for the call initiator, in case they have multiple calls out
            _CallOffers.RemoveAll(c => c.Caller.ConnectionId == callingUser.ConnectionId);


            // Create a new call to match these folks up
            _UserCalls.Add(new UserCall
            {
                Users = new List<ApplicationUser> { callingUser, answeringUser }
            });

            // Tell the original caller that the call was accepted
            await Clients.Client(callerConnectionId.ConnectionId).CallAccepted(answeringUser);

            // Update the user list, since thes two are now in a call
            await SendUserListUpdate();

        }

        public async Task HangUp()
        {
            var disconnectingUser = _Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);

            if (disconnectingUser == null)
            {
                return;
            }

            var currentCall = GetUserCall(disconnectingUser.ConnectionId);

            // Send a hang up message to each user in the call, if there is one
            if (currentCall != null)
            {
                foreach (var user in currentCall.Users.Where(u => u.ConnectionId != disconnectingUser.ConnectionId))
                {
                    await Clients.Client(user.ConnectionId).CallEnded(disconnectingUser, string.Format("{0} {1} has hung up.", disconnectingUser.FirstName, disconnectingUser.LastName));
                }

                // Remove the call from the list if there is only one (or none) person left. This should
                // always trigger now, but will be useful when we implement conferencing.
                currentCall.Users.RemoveAll(u => u.ConnectionId == disconnectingUser.ConnectionId);
                if (currentCall.Users.Count < 2)
                {
                    _UserCalls.Remove(currentCall);
                }
            }

            // Remove all offers initiating from the caller
            _CallOffers.RemoveAll(c => c.Caller.ConnectionId == disconnectingUser.ConnectionId);

            await SendUserListUpdate();
        }

        // WebRTC Signal Handler
        public async Task SendSignal(string signal, string targetConnectionId)
        {
            var callingUser = _Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = _Users.SingleOrDefault(u => u.ConnectionId == targetConnectionId);

            // Make sure both users are valid
            if (callingUser == null || targetUser == null)
            {
                return;
            }

            // Make sure that the person sending the signal is in a call
            var userCall = GetUserCall(callingUser.ConnectionId);

            // ...and that the target is the one they are in a call with
            if (userCall != null && userCall.Users.Exists(u => u.ConnectionId == targetUser.ConnectionId))
            {
                // These folks are in a call together, let's let em talk WebRTC
                await Clients.Client(targetConnectionId).ReceiveSignal(callingUser, signal);
            }
        }

        #region Private Helpers

        private async Task SendUserListUpdate()
        {
            _Users.ForEach(u => u.InCall = (GetUserCall(u.ConnectionId) != null));
            await Clients.All.UpdateUserList(_Users);
        }

        private UserCall GetUserCall(string connectionId)
        {
            var matchingCall =
            _UserCalls.SingleOrDefault(uc => uc.Users.SingleOrDefault(u => u.ConnectionId == connectionId) != null);
            return matchingCall;
        }

        #endregion
    }
    public interface IConnectionHub
    {
        Task UpdateUserList(List<ApplicationUser> userList);
        Task CallAccepted(ApplicationUser acceptingUser);
        Task CallDeclined(ApplicationUser decliningUser, string reason);
        Task IncomingCall(ApplicationUser callingUser);
        Task ReceiveSignal(ApplicationUser signalingUser, string signal);
        Task CallEnded(ApplicationUser signalingUser, string signal);
    }
}