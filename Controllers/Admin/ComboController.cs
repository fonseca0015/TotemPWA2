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
    // /Admin/Combo/List
    [Route("Admin/[controller]/[action]")]
    public class ComboController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ComboController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var combos = await _context.Combos
                .Include(c => c.ProductCombo)
                .Include(c => c.Product)
                .ToListAsync();

            var viewModel = combos.Select(c => new ComboItemViewModel
            {
                Id = c.Id,
                ProductComboName = c.ProductCombo?.Name,
                ProductName = c.Product?.Name
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
            return View(new Combo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductComboId,ProductId")] Combo combo)
        {
            if (combo == null)
            {
                ModelState.AddModelError("", "Combo não foi enviado corretamente.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Products = _context.Products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
                return View(combo ?? new Combo());
            }
            _context.Combos.Add(combo);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var combo = await _context.Combos.FindAsync(id);
            if (combo == null) return NotFound();
            ViewBag.Products = _context.Products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
            return View(combo);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductComboId,ProductId")] Combo combo)
        {
            if (combo == null)
            {
                ModelState.AddModelError("", "Combo não foi enviado corretamente.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Products = _context.Products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
                return View(combo ?? new Combo());
            }

            _context.Combos.Update(combo);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var combo = await _context.Combos
                .Include(c => c.ProductCombo)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (combo == null) return NotFound();
            var viewModel = new ComboItemViewModel
            {
                Id = combo.Id,
                ProductComboName = combo.ProductCombo?.Name,
                ProductName = combo.Product?.Name
            };
            return View(viewModel);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var combo = await _context.Combos.FindAsync(id);
            if (combo == null) return NotFound();
            _context.Combos.Remove(combo);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
}
