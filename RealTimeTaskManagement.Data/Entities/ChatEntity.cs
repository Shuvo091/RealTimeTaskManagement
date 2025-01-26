using System.ComponentModel.DataAnnotations;

namespace RealTimeTaskManagement.Data.Entities
{
    public class ChatEntity : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Message { get; set; } = string.Empty;
        [Required]
        public DateTimeOffset Timestamp { get; set; }
    }
}
