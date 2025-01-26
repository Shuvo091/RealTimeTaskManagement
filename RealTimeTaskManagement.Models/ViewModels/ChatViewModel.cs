namespace RealTimeTaskManagement.Models.ViewModels
{
    public class ChatViewModel
    {
        public IEnumerable<ChatMessageViewModel> Messages { get; set; } = [];
    }
}
