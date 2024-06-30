using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RealTimeTaskManagement.Common.Utilities
{
    public static class Role
    {
        public const string Admin = "Admin";

        public const string Manager = "Manager";

        public const string User = "User";

        public static IEnumerable<SelectListItem> GetAllRoles(bool isSysAdminLoggedIn = false)
        {
            IEnumerable<SelectListItem> roles = typeof(Role).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.IsLiteral && !f.IsInitOnly) // all the constant fields
                .Select(f => new SelectListItem() //convert to SelectListItem
                {
                    Text = f.GetRawConstantValue()?.ToString(),
                    Value = f.GetRawConstantValue()?.ToString()
                });

            if (!isSysAdminLoggedIn)
            {
                roles = roles.Where(x => x.Value != Role.Admin);
            }

            return roles;
        }
    }
}
