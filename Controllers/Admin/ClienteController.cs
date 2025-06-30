using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;
using TotemPWA.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace TotemPWA.Controllers.Admin
{
    // /Admin/Cliente/List
    [Route("Admin/[controller]/[action]")]
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var clients = await _context.Clients.ToListAsync();
            return View(clients);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Client());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CPF")] Client client)
        {
            if (!ModelState.IsValid)
                return View(client);
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return NotFound();
            return View(client);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CPF")] Client client)
        {
            if (!ModelState.IsValid)
                return View(client);
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return NotFound();
            return View(client);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return NotFound();
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
} 