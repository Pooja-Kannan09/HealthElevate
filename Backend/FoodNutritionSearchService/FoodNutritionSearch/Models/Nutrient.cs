namespace FoodNutritionSearch.Models
{
    public class Nutrient
    {
        public ConfidenceRange ConfidenceRange95Percent { get; set; }
        public double StandardDeviation { get; set; }
        public string Unit { get; set; }
        public double Value { get; set; }
    }
}
