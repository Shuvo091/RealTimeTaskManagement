﻿using RealTimeTaskManagement.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Data.Entities
{
    public class TicketEntity : BaseEntity
    {
        public Priority Priority { get; set; }
        public Guid Assignee { get; set; }
        public Guid Reporter {  get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public TimeSpan LoggedMinutes { get; set; }
        public int? ParentTicketId { get; set; }
        public TicketEntity? ParentTicket { get; set; }
    }
}
