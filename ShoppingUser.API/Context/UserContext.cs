using Microsoft.EntityFrameworkCore;

namespace ShoppingUser.API
{
    public class UserContext : DbContext
    {
        public DbSet<ShoppingUserModel> ShoppingUser { get; set; }
        public UserContext(DbContextOptions<UserContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
