namespace RealTimeTaskManagement.Models.ViewModels
{
    public class ChatMessageViewModel : BaseVM
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTimeOffset Timestamp { get; set; }
    }
}
