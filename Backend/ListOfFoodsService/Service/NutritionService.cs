using NutritionDb_Approach.Models;
using NutritionDb_Approach.Repository;

namespace NutritionDb_Approach.Service
{
    public class NutritionService : INutritionService
    {
        private readonly INutritionRepository _nutritionRepository;

        public NutritionService(INutritionRepository nutritionRepository)
        {
            _nutritionRepository = nutritionRepository;
        }

        public async Task<IEnumerable<Mytable>> GetAllAsync()
        {
            return await _nutritionRepository.GetAllAsync();
        }
    
}
}
