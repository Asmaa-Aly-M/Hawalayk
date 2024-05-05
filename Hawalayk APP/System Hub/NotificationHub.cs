////using Microsoft.AspNetCore.SignalR;

////namespace Hawalayk_APP.System_Hub
////{
////    public class NotificationHub : Hub
////    {
////        public Task AddToGroup(string groupName)/////هل اخليها enum بدلا من string
////        {
////            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
////        }


////        public async Task ReceiveNotification(string userId, string message)
////        {

////            await Clients.User(userId).SendAsync("ReceiveNotification", message);
////        }
////        /* public  Task SendNotificationToGroup(string groupName, string message)/////هل اخليها enum بدلا من string
////         {
////             return Clients.Group(groupName).SendAsync("ReceiveNotification", message);
////         }*/

////    }
////}
//using Hawalayk_APP.DataTransferObject;
//using Hawalayk_APP.Models;
//using Hawalayk_APP.Services;
//using Microsoft.AspNetCore.SignalR;

//public class NotificationHub : Hub
//{
//    private readonly IServiceRequestRepository _serviceRequest; // Dependency for post service

//    public NotificationHub(IServiceRequestRepository postService)
//    {
//        _serviceRequest = postService;
//    }

//    public async Task SendNotification (ServiceRequestDTO newPost)
//    {
//        // Filter out the user who created the post
//         var excludedConnectionIds = Clients.All.Except(Context.ConnectionId).ToList();

//        // Get usernames of recipients for notification message
//        var recipients = await _serviceRequest.GetRecipientUsernames(newPost.CustomerId);

//        // Send notification with post and recipient usernames
//        await Clients.SendAsync(
//            "ReceiveNotification", newPost, recipients);
//    }
//}
//    private readonly ICraftRepository _craftService; // Dependency for user service (optional)
//                                                     // private readonly IPostService _postService; // Dependency for post service (not used here)

//    public NotificationHub(ICraftRepository craftRepo) // Inject user service if needed
//    {
//        _craftService = craftRepo;
//    }

//    public async Task SendServiceRequestNotification(ServiceRequestDTO request)
//    {
//        // Filter users based on craft name (replace with your logic)
//        IEnumerable<CraftsmanDTO> recipientUser = await _craftService.GetCraftsmenOfACraft(request.craftName);
//        ;
//        List<string> recipientUserIds = new List<string>();
//        foreach (var recipient in recipientUser)
//        {
//            recipientUserIds.Add(recipient.Id);
//        }

//        // Send notification to recipient users
//        await Clients.Clients(recipientUserIds).SendAsync(
//            "ReceiveServiceRequestNotification", request);
//    }
//}

using Hawalayk_APP.Services;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Hawalayk_APP.System_Hub
{
    public class NotificationHub : Hub
    {

        private readonly ICraftsmenRepository _craftsmanService;
        private readonly IServiceRequestRepository _serviceRequestRepo;

        public NotificationHub(ICraftsmenRepository craftsmenRepo, IServiceRequestRepository serviceRequestRepo)
        {
            _craftsmanService = craftsmenRepo;
            _serviceRequestRepo = serviceRequestRepo;

        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReciveMessage", $"{Context.ConnectionId} has joined");
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //var craftsman = await _craftsmanService.GetById(userId);
            //if (craftsman != null)
            //{
            //    await Groups.AddToGroupAsync(craftsman.Craft.Name.ToString(), Context.ConnectionId);
            //}
        }
        public async Task CreateServiceRequest(string requestDTO)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //   await _serviceRequestRepo.CreateAsync(userId, requestDTO);
            Clients.All.SendAsync("serveceRequestAdded", requestDTO);
            //      Clients.("serveceRequestAdded" ,requestDTO);

        }

    }
}

