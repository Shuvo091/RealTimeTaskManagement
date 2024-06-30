using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; } = null!;
        [PersonalData]
        public string LastName { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        [NotMapped]
        public string FullNameUS => $"{LastName}, {FirstName}";
    }
}
