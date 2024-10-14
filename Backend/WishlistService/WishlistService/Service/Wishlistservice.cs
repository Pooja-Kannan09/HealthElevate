using Microsoft.EntityFrameworkCore;
using WishlistService.Models;
using WishlistService.Repository;

namespace WishlistService.Service
{
    public class Wishlistservice : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public Wishlistservice(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task<IEnumerable<WishlistItem>> GetWishlistItems(int UserId)
        {
            return await _wishlistRepository.GetWishlistItems(UserId);
        }

        public async Task<bool> IsItemInWishlist(int foodid, int userId)
        {
            return await _wishlistRepository.IsItemInWishlist(foodid, userId);
        }
        public async Task<WishlistItem> AddToWishlist(WishlistItem item )
        {
            return await _wishlistRepository.AddToWishlist(item);
        }

        public async Task<bool> RemoveFromWishlist(int favId, int UserId)
        {
            return await _wishlistRepository.RemoveFromWishlist(favId, UserId);
        }

    }
}
