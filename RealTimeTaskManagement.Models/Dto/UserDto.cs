using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Models.Dto
{
    public class UserDto : BaseDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public string FullName => $"{FirstName} {LastName}";
        public string FullNameUS => $"{LastName}, {FirstName}";
    }
}
