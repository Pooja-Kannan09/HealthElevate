using Microsoft.EntityFrameworkCore;
using System;

namespace NutritionDb_Approach.Models
{
    public class NutritionDbContext : DbContext
    {
        public NutritionDbContext(DbContextOptions<NutritionDbContext> options) : base(options)
        {
        }
        public DbSet<Mytable> Mytables { get; set; }
    }
}
