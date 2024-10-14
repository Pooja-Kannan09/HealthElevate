using FoodNutritionSearch.Models;
using FoodNutritionSearch.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Text.Json;
using System.Net;
using RichardSzalay.MockHttp;
using Moq.Protected;
using Xunit;


namespace NutritionSearchTest
{
    public class NutritionSearchTest
    {
        private readonly MockHttpMessageHandler _mockHttpMessageHandler;
        private readonly NutritionService _nutritionService;

        public NutritionSearchTest()
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();

            var httpClient = new HttpClient(_mockHttpMessageHandler);

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SpoonacularApiKey"]).Returns("fake_api_key");

            _nutritionService = new NutritionService(httpClient, configurationMock.Object);
        }

        [Fact]
        public async Task GetNutritionByFoodTitleAsync_ReturnsNutritionResult()
        {
            // Arrange
            var expectedNutritionResult = new NutritionResult
            {
                Calories = new Nutrient { Value = 95, Unit = "calories" },
                Carbs = new Nutrient { Value = 25, Unit = "grams" },
                Protein = new Nutrient { Value = 0.5, Unit = "grams" },
                Fat = new Nutrient { Value = 0.3, Unit = "grams" },
                RecipesUsed = 1
            };

            var jsonResponse = JsonSerializer.Serialize(expectedNutritionResult);

            _mockHttpMessageHandler.When("https://api.spoonacular.com/recipes/guessNutrition*")
                .Respond("application/json", jsonResponse);

            // Act
            var actualNutritionResult = await _nutritionService.GetNutritionByFoodTitleAsync("sample food");

            // Assert
            Assert.NotNull(actualNutritionResult);
            Assert.Equal(expectedNutritionResult.Calories.Value, actualNutritionResult.Calories.Value);
            Assert.Equal(expectedNutritionResult.Carbs.Value, actualNutritionResult.Carbs.Value);
            Assert.Equal(expectedNutritionResult.Protein.Value, actualNutritionResult.Protein.Value);
            Assert.Equal(expectedNutritionResult.Fat.Value, actualNutritionResult.Fat.Value);
            Assert.Equal(expectedNutritionResult.RecipesUsed, actualNutritionResult.RecipesUsed);
        }
    }
}