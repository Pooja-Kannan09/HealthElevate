using Microsoft.AspNetCore.Mvc;
using FoodNutritionSearch.Services;
using System.Threading.Tasks;
using FoodNutritionSearch.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace FoodNutritionSearch.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NutritionController : ControllerBase
    {
        private readonly INutritionService _nutritionService;

        public NutritionController(INutritionService nutritionService)
        {
            _nutritionService = nutritionService;
        }
        [HttpGet("guessNutrition")]
        public async Task<IActionResult> GuessNutrition(string title)
        {
            
            try
            {
                var nutritionResult = await _nutritionService.GetNutritionByFoodTitleAsync(title);

                // If the result is null or empty, throw a custom exception
                if (nutritionResult == null)
                {
                    throw new NutritionNotFoundException($"No nutrition data found for the food title: {title}");
                }

                return Ok(nutritionResult);
            }
            catch (NutritionNotFoundException ex)
            {
                // Handle the custom exception and return NotFound response
                return NotFound(new { Message = ex.Message });
            }
            catch (System.Exception ex)
            {
                // Handle any other exceptions and return 500 status
                return StatusCode(500, new { Message = "Internal server error: " + ex.Message });
            }
        }
    }
}
