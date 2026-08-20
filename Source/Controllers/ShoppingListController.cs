
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Source.Models.GroceryList;

public class ShoppingListController : Controller
{
    private readonly SourceContext _context;

    public ShoppingListController(SourceContext context)
    {
        _context = context;
    }

    // GET: SHOPPINGLISTS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.ShoppingList.ToListAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GenerateShoppingList(int mealPlanId)
    {
        var listId = 1;

        return RedirectToAction(nameof(Index));
        //return View(Details(listId));
    }

    // GET: SHOPPINGLISTS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shoppinglist = await _context.ShoppingList
            .FirstOrDefaultAsync(m => m.Id == id);
        if (shoppinglist == null)
        {
            return NotFound();
        }

        return View(shoppinglist);
    }

    // GET: SHOPPINGLISTS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SHOPPINGLISTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Completed,UserId,User,Items")] ShoppingList shoppinglist)
    {
        if (ModelState.IsValid)
        {
            _context.Add(shoppinglist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(shoppinglist);
    }

    // GET: SHOPPINGLISTS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shoppinglist = await _context.ShoppingList.FindAsync(id);
        if (shoppinglist == null)
        {
            return NotFound();
        }
        return View(shoppinglist);
    }

    // POST: SHOPPINGLISTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Completed,UserId,User,Items")] ShoppingList shoppinglist)
    {
        if (id != shoppinglist.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(shoppinglist);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListExists(shoppinglist.Id))
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
        return View(shoppinglist);
    }

    // GET: SHOPPINGLISTS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shoppinglist = await _context.ShoppingList
            .FirstOrDefaultAsync(m => m.Id == id);
        if (shoppinglist == null)
        {
            return NotFound();
        }

        return View(shoppinglist);
    }

    // POST: SHOPPINGLISTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var shoppinglist = await _context.ShoppingList.FindAsync(id);
        if (shoppinglist != null)
        {
            _context.ShoppingList.Remove(shoppinglist);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ShoppingListExists(int? id)
    {
        return _context.ShoppingList.Any(e => e.Id == id);
    }
}
