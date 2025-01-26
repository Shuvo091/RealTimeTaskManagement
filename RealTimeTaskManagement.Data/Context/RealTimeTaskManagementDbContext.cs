using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTimeTaskManagement.Data.Entities;

namespace RealTimeTaskManagement.Data.Context
{
    public class RealTimeTaskManagementDbContext : IdentityDbContext<UserEntity>
    {
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<ChatEntity> Chat { get; set; }


        public RealTimeTaskManagementDbContext(DbContextOptions<RealTimeTaskManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ChatEntity>(entity =>
            {
                entity.HasOne(c => c.EnteredBy)
                    .WithMany()
                    .HasForeignKey(c => c.EnteredById)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.ModifiedBy)
                    .WithMany()
                    .HasForeignKey(c => c.ModifiedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<TicketEntity>(entity =>
            {
                entity.HasOne(c => c.EnteredBy)
                    .WithMany()
                    .HasForeignKey(c => c.EnteredById)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.ModifiedBy)
                    .WithMany()
                    .HasForeignKey(c => c.ModifiedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Fix Identity table column size issue
            builder.Entity<IdentityUserToken<string>>(b =>
            {
                b.Property(u => u.LoginProvider).HasMaxLength(128);
                b.Property(u => u.Name).HasMaxLength(128);
            });

            builder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.Property(u => u.LoginProvider).HasMaxLength(128);
                b.Property(u => u.ProviderKey).HasMaxLength(128);
            });
        }

    }
}
