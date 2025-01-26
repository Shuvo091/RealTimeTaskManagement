using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Data.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public string EnteredById { get; set; }
        public virtual UserEntity EnteredBy { get; set; }

        public string ModifiedById { get; set; }
        public virtual UserEntity ModifiedBy { get; set; }

        public DateTimeOffset EnteredOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
    }
}
