namespace NutritionDb_Approach.Models
{
    public class Mytable
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Caloric { get; set; }
        public string Type { get; set; } = null!;
        public int Fat { get; set; }
        public int Carbon { get; set; }
        public int Protein { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; } = null!;
    }
}
