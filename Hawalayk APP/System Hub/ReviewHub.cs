using Hawalayk_APP.Models;
using Microsoft.AspNetCore.SignalR;

namespace Hawalayk_APP.System_Hub
{
    public class ReviewHub :  Hub
    {
        public async Task SendReviewAsync(Review reviewdata)
        {
            // Broadcast the new post to all connected clients
            await Clients.All.SendAsync("ReceiveReview", reviewdata);
        }
    }
}
