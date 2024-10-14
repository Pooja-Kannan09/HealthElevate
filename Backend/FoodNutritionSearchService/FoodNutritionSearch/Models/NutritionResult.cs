namespace FoodNutritionSearch.Models
{
    public class NutritionResult
    {
        public Nutrient Calories { get; set; }
        public Nutrient Carbs { get; set; }
        public Nutrient Fat { get; set; }
        public Nutrient Protein { get; set; }
        public int RecipesUsed { get; set; }
    }
}
