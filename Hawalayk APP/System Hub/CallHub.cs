using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

public class CallHub : Hub
{
    public async Task SendOffer(string targetUserId, string offer)
    {
        string userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await Clients.User(targetUserId).SendAsync("ReceiveOffer", offer, userId);
    }

    public async Task SendAnswer(string targetUserId, string answer)
    {
        string userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await Clients.User(targetUserId).SendAsync("ReceiveAnswer", answer, userId);
    }

    public async Task SendIceCandidate(string targetUserId, string candidate)
    {
        string userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await Clients.User(targetUserId).SendAsync("ReceiveIceCandidate", candidate, userId);
    }
}
