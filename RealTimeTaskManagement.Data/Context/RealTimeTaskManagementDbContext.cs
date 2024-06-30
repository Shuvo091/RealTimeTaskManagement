using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTimeTaskManagement.Data.Entities;

namespace RealTimeTaskManagement.Data.Context
{
    public class RealTimeTaskManagementDbContext : IdentityDbContext<User>
    {
        public DbSet<Ticket> Tickets { get; set; }

        public RealTimeTaskManagementDbContext(DbContextOptions<RealTimeTaskManagementDbContext> options)
            : base(options)
        {
        }
    }
}
