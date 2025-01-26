using Microsoft.AspNetCore.SignalR;
using RealTimeTaskManagement.Data.Context;
using RealTimeTaskManagement.Data.Entities;
using System.Security.Claims;

namespace ChatService
{
    public class ChatHub : Hub
    {
        private readonly RealTimeTaskManagementDbContext _context;

        public ChatHub(RealTimeTaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string message)
        {
            // Get the user ID from the claims
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User is not authenticated.");
            }

            // Save the message to the database
            var chatMessage = new ChatEntity
            {
                UserId = new Guid(userId),
                Message = message,
                Timestamp = DateTime.UtcNow,
                EnteredById = userId,
                ModifiedById = userId,
                EnteredOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };

            _context.Chat.Add(chatMessage);
            await _context.SaveChangesAsync();

            // Broadcast the message to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", userId ?? "Unknown User", message);
        }
    }
}
