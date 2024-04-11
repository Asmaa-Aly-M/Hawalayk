using Hawalayk_APP.Models;
using Microsoft.AspNetCore.SignalR;

namespace Hawalayk_APP.System_Hub
{
    public class PostHub:Hub
    {
        public async Task SendReviewAsync(Post post)
        {
            // Broadcast the new review to all connected clients
            await Clients.All.SendAsync("ReceivePost", post);
        }
    }
}
