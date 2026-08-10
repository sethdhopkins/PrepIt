using System.ComponentModel.DataAnnotations;

namespace Source.Models
{
    public class Meal
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string? Type { get; set; }
        public bool Cooked { get; set; }
        public int ApiId { get; set; }
        public int? MealPlanId { get; set; }
    }
}
