using NutritionDb_Approach.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using NutritionDb_Approach.Models;
using Microsoft.EntityFrameworkCore;

namespace NutritionDb_Approach.Repository



{
    public class NutritionRepository : INutritionRepository
    {
        private readonly NutritionDbContext _context;

        public NutritionRepository(NutritionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mytable>> GetAllAsync()
        {
            return await _context.Mytables.ToListAsync();
        }
    }
}
