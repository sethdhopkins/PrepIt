using Source.Models.MealPlanning;

public class RecipeQueueViewModel
{
    public IEnumerable<MealPlan> Plan { get; set; } = [];
    public IEnumerable<RecipeQueue> Recipes { get; set; } = [];

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

    public DateTime WeekStart { get; set; }
    public DateTime WeekEnd { get; set; }
}