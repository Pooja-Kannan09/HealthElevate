using NutritionDb_Approach.Models;

namespace NutritionDb_Approach.Repository
{
    public interface INutritionRepository   
    {
        Task<IEnumerable<Mytable>> GetAllAsync();
        
    }
}
