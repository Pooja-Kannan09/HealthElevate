using WishlistService.Models;

namespace WishlistService.Service
{
    public interface IWishlistService
    {
        Task<IEnumerable<WishlistItem>> GetWishlistItems(int UserId);

        //Task<IEnumerable<WishlistItem>> GetWishlistItems();
        Task<WishlistItem> AddToWishlist(WishlistItem item);
        Task<bool> IsItemInWishlist(int foodid, int userId);
        Task<bool> RemoveFromWishlist(int  favId,int UserId);
    }
}
