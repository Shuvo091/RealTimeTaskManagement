using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Models.Dto
{
    public class BaseDto
    {
        public int Id { get; set; }
        public string? EnteredById { get; set; }
        public string? ModifiedById { get; set; }
        public DateTimeOffset EnteredOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
    }
}
