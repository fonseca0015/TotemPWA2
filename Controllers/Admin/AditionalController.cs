using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using TotemPWA.Data;
using TotemPWA.Models;
using TotemPWA.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace TotemPWA.Controllers.Admin
{
    // /Admin/Aditional/List
    [Route("Admin/[controller]/[action]")]
    public class AditionalController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AditionalController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var additionals = await _context.Additionals
                .Include(a => a.Product)
                .Include(a => a.Ingredient)
                .ToListAsync();
            var viewModel = additionals.Select(a => new AditionalItemViewModel
            {
                ProductId = a.ProductId,
                ProductName = a.Product?.Name,
                IngredientId = a.IngredientId,
                IngredientName = a.Ingredient?.Name
            }).ToList();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new AditionalViewModel
            {
                Products = _context.Products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList(),
                Ingredients = _context.Ingredients.Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AditionalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Products = _context.Products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
                viewModel.Ingredients = _context.Ingredients.Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToList();
                return View(viewModel);
            }
            _context.Additionals.Add(viewModel.Additional);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int productId, int ingredientId)
        {
            var additional = await _context.Additionals.FindAsync(productId, ingredientId);
            if (additional == null) return NotFound();
            var viewModel = new AditionalViewModel
            {
                Additional = additional,
                Products = _context.Products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList(),
                Ingredients = _context.Ingredients.Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AditionalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Products = _context.Products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
                viewModel.Ingredients = _context.Ingredients.Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToList();
                return View(viewModel);
            }
            _context.Additionals.Update(viewModel.Additional);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int productId, int ingredientId)
        {
            var additional = await _context.Additionals
                .Include(a => a.Product)
                .Include(a => a.Ingredient)
                .FirstOrDefaultAsync(a => a.ProductId == productId && a.IngredientId == ingredientId);
            if (additional == null) return NotFound();
            var viewModel = new AditionalItemViewModel
            {
                ProductId = additional.ProductId,
                ProductName = additional.Product?.Name,
                IngredientId = additional.IngredientId,
                IngredientName = additional.Ingredient?.Name
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int productId, int ingredientId)
        {
            var additional = await _context.Additionals.FindAsync(productId, ingredientId);
            if (additional == null) return NotFound();
            _context.Additionals.Remove(additional);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet("/api/adicionais/por-produto/{productId}")]
        public async Task<IActionResult> GetAdditionalsByProduct(int productId)
        {
            var additionals = await _context.Additionals
                .Include(a => a.Ingredient)
                .Where(a => a.ProductId == productId)
                .ToListAsync();
                
            var result = additionals.Select(a => new {
                IngredientId = a.IngredientId,
                IngredientName = a.Ingredient != null ? a.Ingredient.Name : "Ingrediente não encontrado",
                Price = a.Ingredient != null ? a.Ingredient.Price : 0
            }).ToList();
            
            return Json(result);
        }
    }
} 