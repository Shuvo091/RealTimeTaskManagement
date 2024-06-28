using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Data.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public Guid EnteredById { get; set; }
        public virtual User EnteredBy { get; set; }
        public Guid ModifiedById { get; set; }
        public virtual User ModifiedBy { get; set; }

        public DateTimeOffset EnteredOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
    }
}
