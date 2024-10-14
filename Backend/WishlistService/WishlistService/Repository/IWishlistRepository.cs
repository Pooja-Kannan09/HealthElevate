using WishlistService.Models;

namespace WishlistService.Repository
{
    public interface IWishlistRepository
    {
        Task<IEnumerable<WishlistItem>> GetWishlistItems(int UserId);
        //Task<IEnumerable<WishlistItem>> GetWishlistItems();

        Task<WishlistItem> AddToWishlist(WishlistItem item);
        Task<bool> RemoveFromWishlist(int favId, int UserId);
        Task<bool> IsItemInWishlist(int foodid, int userId);
    }
}
