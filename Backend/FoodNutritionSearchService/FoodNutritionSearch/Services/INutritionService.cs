using System.Threading.Tasks;
using FoodNutritionSearch.Models;

namespace FoodNutritionSearch.Services
{
    public interface INutritionService
    {
        Task<NutritionResult> GetNutritionByFoodTitleAsync(string title);
    }
}
