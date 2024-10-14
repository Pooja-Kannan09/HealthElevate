using Moq;
using WishlistService.Models;
using WishlistService.Repository;
using WishlistService.Service;


namespace WishlistServiceTest
{
    public class WishlistTest
    {
        private readonly Mock<IWishlistRepository> _wishlistRepositoryMock;
        private readonly Wishlistservice _wishlistService;

        public WishlistTest()
        {
            _wishlistRepositoryMock = new Mock<IWishlistRepository>();
            _wishlistService = new Wishlistservice(_wishlistRepositoryMock.Object);
        }

        [Fact]
        public async Task GetWishlistItems_ReturnsWishlistItems_WhenUserIdIsValid()
        {
            // Arrange
            int userId = 1;
            var wishlistItems = new List<WishlistItem>
            {
                new WishlistItem { favId = 1, foodid = 101, UserId = userId },
                new WishlistItem { favId = 2, foodid = 102, UserId = userId }
            };

            _wishlistRepositoryMock
                .Setup(repo => repo.GetWishlistItems(userId))
                .ReturnsAsync(wishlistItems);

            // Act
            var result = await _wishlistService.GetWishlistItems(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<WishlistItem>)result).Count);
        }

        [Fact]
        public async Task IsItemInWishlist_ReturnsTrue_WhenItemExists()
        {
            // Arrange
            int foodId = 101;
            int userId = 1;

            _wishlistRepositoryMock
                .Setup(repo => repo.IsItemInWishlist(foodId, userId))
                .ReturnsAsync(true);

            // Act
            var result = await _wishlistService.IsItemInWishlist(foodId, userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddToWishlist_ReturnsAddedItem_WhenItemIsValid()
        {
            // Arrange
            var newItem = new WishlistItem { favId = 3, foodid = 103, UserId = 1 };

            _wishlistRepositoryMock
                .Setup(repo => repo.AddToWishlist(newItem))
                .ReturnsAsync(newItem);

            // Act
            var result = await _wishlistService.AddToWishlist(newItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newItem.foodid, result.foodid);
        }

        [Fact]
        public async Task RemoveFromWishlist_ReturnsTrue_WhenItemIsRemovedSuccessfully()
        {
            // Arrange
            int favId = 2;
            int userId = 1;

            _wishlistRepositoryMock
                .Setup(repo => repo.RemoveFromWishlist(favId, userId))
                .ReturnsAsync(true);

            // Act
            var result = await _wishlistService.RemoveFromWishlist(favId, userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveFromWishlist_ReturnsFalse_WhenItemDoesNotExist()
        {
            // Arrange
            int favId = 2;
            int userId = 1;

            _wishlistRepositoryMock
                .Setup(repo => repo.RemoveFromWishlist(favId, userId))
                .ReturnsAsync(false);

            // Act
            var result = await _wishlistService.RemoveFromWishlist(favId, userId);

            // Assert
            Assert.False(result);
        }
    }
}
