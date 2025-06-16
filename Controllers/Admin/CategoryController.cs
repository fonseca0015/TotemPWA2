using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using TotemPWA.Models;
using TotemPWA.Data;
using TotemPWA.ViewModels;

namespace TotemPWA.Controllers.Admin
{
    // /admin/category/list 
    [Route("Admin/[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            var categories = _context.Categories
                .Include(c => c.Subcategories)
                .ToList();

            return View(categories);
        }

    }
}