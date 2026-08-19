using Source.Models.MealPlanning;
using Source.Models.Recipes;

public class RecipeQueueViewModel
{
    public MealPlan Plan { get; set; } = null!;
    public IEnumerable<Recipe> Recipes { get; set; } = null!;

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

    public DateOnly WeekStart { get; set; }
    public DateOnly WeekEnd { get; set; }
    public IEnumerable<DateOnly> WeekDays =>
        Enumerable.Range(0, 7)
            .Select(i => WeekStart.AddDays(i));
    public bool IsCurrentWeek
    {
        get
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var currentSunday = today.AddDays(-(int)today.DayOfWeek);

            return WeekStart == currentSunday;
        }
    }
}