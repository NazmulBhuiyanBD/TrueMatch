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
        public DbSet<FriendRequest> FriendRequests { get; set; }


    }
}