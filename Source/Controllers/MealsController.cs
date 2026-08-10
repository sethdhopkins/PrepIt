
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Source.Models;

public class MealsController : Controller
{
    private readonly SourceContext _context;

    public MealsController(SourceContext context)
    {
        _context = context;
    }

    // GET: MEALS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Meal.ToListAsync());
    }

    // GET: MEALS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var meal = await _context.Meal
            .FirstOrDefaultAsync(m => m.Id == id);
        if (meal == null)
        {
            return NotFound();
        }

        return View(meal);
    }

    // GET: MEALS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MEALS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Date,Type,Cooked,ApiId,MealPlanId")] Meal meal)
    {
        if (ModelState.IsValid)
        {
            _context.Add(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(meal);
    }

    // GET: MEALS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var meal = await _context.Meal.FindAsync(id);
        if (meal == null)
        {
            return NotFound();
        }
        return View(meal);
    }

    // POST: MEALS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Date,Type,Cooked,ApiId,MealPlanId")] Meal meal)
    {
        if (id != meal.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(meal);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(meal.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(meal);
    }

    // GET: MEALS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var meal = await _context.Meal
            .FirstOrDefaultAsync(m => m.Id == id);
        if (meal == null)
        {
            return NotFound();
        }

        return View(meal);
    }

    // POST: MEALS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var meal = await _context.Meal.FindAsync(id);
        if (meal != null)
        {
            _context.Meal.Remove(meal);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MealExists(int? id)
    {
        return _context.Meal.Any(e => e.Id == id);
    }
}
