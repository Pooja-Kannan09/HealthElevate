using Moq;
using NutritionDb_Approach.Models;
using NutritionDb_Approach.Repository;
using NutritionDb_Approach.Service;

namespace NutritionListTest
{
    public class NutritionlistTest
    {
        private readonly Mock<INutritionRepository> _nutritionRepositoryMock;
        private readonly NutritionService _nutritionService;

        public NutritionlistTest()
        {
            _nutritionRepositoryMock = new Mock<INutritionRepository>();
            _nutritionService = new NutritionService(_nutritionRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllNutritionItems_WhenCalled()
        {
            // Arrange
            var nutritionItems = new List<Mytable>
            {
                new Mytable { Id = 1, Name = "Apple", Caloric = 95 },
                new Mytable { Id = 2, Name = "Banana", Caloric = 105 }
            };

            _nutritionRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(nutritionItems);

            // Act
            var result = await _nutritionService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<Mytable>)result).Count);
            Assert.Equal("Apple", ((List<Mytable>)result)[0].Name);
            Assert.Equal("Banana", ((List<Mytable>)result)[1].Name);
        }
    }
}
