using Microsoft.EntityFrameworkCore;

namespace WishlistService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<WishlistItem> WishlistItems { get; set; }


    }
}
