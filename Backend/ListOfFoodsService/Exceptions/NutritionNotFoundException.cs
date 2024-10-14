namespace NutritionDb_Approach.Exceptions
{
    public class NutritionNotFoundException:Exception
    {
        public NutritionNotFoundException() : base("No nutrition data found.")
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
