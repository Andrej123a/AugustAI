using AugustAI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AugustAI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult IngredientsByCategory(IngredientCategory category)
        {
            var items = IngredientCatalog.Get(category);
            return Json(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitSelection(IngredientCategory category, string ingredientsJson)
        {
            var ingredients = new List<string>();

            if (!string.IsNullOrWhiteSpace(ingredientsJson))
            {
                ingredients = JsonSerializer.Deserialize<List<string>>(ingredientsJson) ?? new List<string>();
            }

            TempData["Result"] = ingredients.Count == 0
                ? $"Category: {category} | No ingredients selected."
                : $"Category: {category} | Ingredients: {string.Join(", ", ingredients)}";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
