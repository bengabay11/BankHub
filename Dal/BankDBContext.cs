using Dal.Models;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class BankDbContext(DbContextOptions<BankDbContext> options) : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<User> BankUsers { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Transfer>()
                .HasOne(t => t.GiverUser)
                .WithMany(u => u.OutgoingTransfers)
                .HasForeignKey(t => t.GiverUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Transfer>()
                .HasOne(t => t.TakerUser)
                .WithMany(u => u.IncomingTransfers)
                .HasForeignKey(t => t.TakerUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
