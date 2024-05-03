using Microsoft.AspNetCore.SignalR;

namespace Hawalayk_APP.System_Hub
{
    public class Notification : Hub
    {
        public Task AddToGroup(string groupName)/////هل اخليها enum بدلا من string
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }


        public async Task ReceiveNotification(string userId, string message)
        {

            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
        /* public  Task SendNotificationToGroup(string groupName, string message)/////هل اخليها enum بدلا من string
         {
             return Clients.Group(groupName).SendAsync("ReceiveNotification", message);
         }*/

    }
}
