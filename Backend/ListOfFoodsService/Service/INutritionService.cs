using System.Collections.Generic;
using System.Threading.Tasks;

using NutritionDb_Approach.Models;

namespace NutritionDb_Approach.Service



{
    public interface INutritionService
    {
        Task<IEnumerable<Mytable>> GetAllAsync();
        // Add other method signatures as needed
    }
}
