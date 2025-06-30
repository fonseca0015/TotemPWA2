using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;

namespace TotemPWA.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;


    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("Menu/{categoryId:int?}/{subcategoryId:int?}")]
    public async Task<IActionResult> Menu(int? categoryId, int? subcategoryId)
    {
        var rootCategoriesRaw = await _context.Categories
            .Where(c => c.ParentCategoryId == null)
            .ToListAsync();

        var activeCategoryId = categoryId;
        if (activeCategoryId == null && rootCategoriesRaw.Count > 0)
            activeCategoryId = rootCategoriesRaw.First().Id;

        var rootCategories = rootCategoriesRaw
            .Select(c => new
            {
                id = c.Id,
                name = c.Name,
                icon = c.Icon,
                active = c.Id == activeCategoryId
            })
            .ToList();

        var subcategoriesRaw = await _context.Categories
            .Where(c => c.ParentCategoryId == activeCategoryId)
            .ToListAsync();

        int? activeSubcategoryId = null;
        if (subcategoriesRaw.Any(c => c.Id == subcategoryId))
            activeSubcategoryId = subcategoryId;
        else if (subcategoriesRaw.Count > 0)
            activeSubcategoryId = subcategoriesRaw.First().Id;

        var subcategories = subcategoriesRaw
            .Select(c => new
            {
                id = c.Id,
                name = c.Name,
                icon = c.Icon,
                active = c.Id == activeSubcategoryId
            })
            .ToList();

        // Buscar produtos normalmente
        var productsRaw = await _context.Products
            .Where(p => p.CategoryId == activeSubcategoryId)
            .ToListAsync();

        // Buscar promoções válidas
        var now = DateTime.Now;
        var promotions = await _context.Promotions
            .Where(p => p.ValidUntil >= now)
            .ToListAsync();

        // Merge em memória: associar promoções aos produtos
        var products = productsRaw.Select(p => {
            var promo = promotions.FirstOrDefault(pr => pr.ProductId == p.Id);
            return new {
                id = p.Id,
                name = p.Name,
                image = p.Image,
                price = promo != null ? p.Price * (1 - (decimal)promo.Percent / 100) : p.Price,
                priceOriginal = p.Price,
                percentPromotion = promo?.Percent
            };
        }).ToList();

        // Buscar combos desta subcategoria
        var combos = await _context.Products
            .Where(p => p.CategoryId == activeSubcategoryId)
            .Select(p => new {
                id = p.Id,
                name = p.Name,
                price = p.Price,
                image = p.Image,
                itens = _context.Combos
                    .Where(c => c.ProductComboId == p.Id)
                    .Select(c => c.Product != null ? c.Product.Name : "")
                    .ToList()
            })
            .Where(p => p.itens.Count > 0)
            .ToListAsync();

        ViewBag.Category = activeCategoryId;
        ViewBag.Categories = rootCategories;
        ViewBag.SubCategories = subcategories;
        ViewBag.Products = products;
        ViewBag.Combos = combos;

        return View("Menu");
    }


    public IActionResult Tela2()
    {
        return View();
    }

    // Adicionado o método de ação para Tela3
    public IActionResult Tela3()
    {
        return View();
    }

    public IActionResult Cpf()
    {
        return View();
    }

    public IActionResult Final()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}