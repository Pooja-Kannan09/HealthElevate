namespace FoodNutritionSearch.Exceptions
{
    public class NutritionNotFoundException:Exception
    {
        public NutritionNotFoundException() : base("The nutrition data for the requested food item could not be found.")
        {
        }

        public NutritionNotFoundException(string message) : base(message)
        {
        }

        public NutritionNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
