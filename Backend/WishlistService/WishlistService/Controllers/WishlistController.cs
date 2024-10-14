using Microsoft.AspNetCore.Mvc;
using WishlistService.Models;
using WishlistService.Service;
using WishlistService.Exceptions;  // Import the custom exception
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WishlistService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }
        
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetWishlistItems(int UserId)
        {
            try
            {
                var items = await _wishlistService.GetWishlistItems(UserId);

                if (items == null )
                {
                    throw new WishlistNotFoundException($"No wishlist items found for User ID: {UserId}");
                }

                return Ok(items);
            }
            catch (WishlistNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistItem item)

        {

            try

            {
                // Check if the item already exists in the wishlist for the current user

                var existingItem = await _wishlistService.IsItemInWishlist(item.foodid, item.UserId);

                if (existingItem)

                {

                    // If the item already exists, return a conflict response

                    return Conflict(new { Message = "Item already exists in the wishlist." });

                }

                // Proceed with adding the item if it doesn't exist

                var addedItem = await _wishlistService.AddToWishlist(item);

                if (addedItem == null)

                {

                    throw new System.Exception("Failed to add item to wishlist.");

                }

                return Ok(addedItem);

            }

            catch (System.Exception ex)

            {

                return StatusCode(500, new { Message = "Internal server error: " + ex.Message });

            }

        }


        [HttpDelete("{foodid}/{UserId}")]
        public async Task<IActionResult> RemoveFromWishlist(int foodid, int UserId)
        {
            try
            {
                var result = await _wishlistService.RemoveFromWishlist(foodid, UserId);

                if (!result)
                {
                    throw new WishlistNotFoundException($"No wishlist item found with Food ID: {foodid} for User ID: {UserId}");
                }

                return Ok();
            }
            catch (WishlistNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error: " + ex.Message });
            }
        }
    }
}
