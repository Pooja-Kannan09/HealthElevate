using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionDb_Approach.Exceptions;
using NutritionDb_Approach.Models;
using NutritionDb_Approach.Service;

namespace NutritionDb_Approach.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionController : ControllerBase
    {
        private readonly INutritionService _nutritionService;

        public NutritionController(INutritionService nutritionService)
        {
            _nutritionService = nutritionService;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mytable>>> GetAll()
        {
            try
            {
                var data = await _nutritionService.GetAllAsync();

                if (data == null)
                {
                    throw new NutritionNotFoundException("No nutrition data found.");
                }

                return Ok(data);
            }
            catch (NutritionNotFoundException ex)
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
