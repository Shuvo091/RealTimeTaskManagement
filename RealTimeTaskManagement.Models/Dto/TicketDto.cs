using RealTimeTaskManagement.Models.Enums;

namespace RealTimeTaskManagement.Models.Dto
{
    public class TicketDto : BaseDto
    {
        public Priority Priority { get; set; }
        public Guid Assignee { get; set; }
        public Guid Reporter { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public TimeSpan LoggedMinutes { get; set; }
        public int? ParentTicketId { get; set; }
    }
}
