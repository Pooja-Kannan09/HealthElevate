using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FoodNutritionSearch.Models;

namespace FoodNutritionSearch.Services
{
    public class NutritionService : INutritionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public NutritionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["SpoonacularApiKey"]; // Retrieve the API key from configuration
        }

        public async Task<NutritionResult> GetNutritionByFoodTitleAsync(string title)
        {
            var response = await _httpClient.GetAsync($"https://api.spoonacular.com/recipes/guessNutrition?title={title}&apiKey={_apiKey}");
            response.EnsureSuccessStatusCode();

            // Read the response content as a string
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response to the NutritionResult model
            var nutritionResult = JsonSerializer.Deserialize<NutritionResult>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // This option allows case-insensitive property matching
            });

            return nutritionResult;
        }
    }
}
