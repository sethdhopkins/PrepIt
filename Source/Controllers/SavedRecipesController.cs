using Microsoft.AspNetCore.Mvc;

namespace Source.Controllers
{
    public class SavedRecipesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
