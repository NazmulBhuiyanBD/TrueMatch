using Microsoft.EntityFrameworkCore;
using TrueMatch.Models.Data;

namespace TrueMatch.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserInfo>()
                .HasOne(u => u.Account)
                .WithOne()
                .HasForeignKey<UserInfo>(u => u.Email)
                .HasPrincipalKey<Account>(a => a.Email);
        }
    }
}
