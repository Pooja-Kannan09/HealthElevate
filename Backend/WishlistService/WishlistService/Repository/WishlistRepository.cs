using Microsoft.EntityFrameworkCore;
using WishlistService.Models;

namespace WishlistService.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly AppDbContext _context;

        public WishlistRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WishlistItem>> GetWishlistItems(int UserId)
        {
            return await _context.WishlistItems.Where(x => x.UserId == UserId).ToListAsync();
        }

        public async Task<WishlistItem> AddToWishlist(WishlistItem item)
        {

            await _context.Set<WishlistItem>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> IsItemInWishlist(int foodid, int userId)
        {
            return await _context.WishlistItems
                                 .AnyAsync(i => i.foodid == foodid && i.UserId == userId);
        }


        public async Task<bool> RemoveFromWishlist(int favId, int UserId)
        {
            var item = await _context.WishlistItems.FirstOrDefaultAsync(x => x.foodid == favId && x.UserId == UserId);
            if (item != null)
            {
                _context.WishlistItems.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
