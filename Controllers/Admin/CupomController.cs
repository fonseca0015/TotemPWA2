using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;

namespace TotemPWA.Controllers.Admin
{
    // /Admin/Cupom/List
    [Route("Admin/[controller]/[action]")]
    public class CupomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CupomController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            var cupons = await _context.Cupons.ToListAsync();
            return View("~/Views/Cupom/List.cshtml", cupons);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cupom cupom)
        {
            if (!ModelState.IsValid) return View(cupom);

            _context.Cupons.Add(cupom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cupom = await _context.Cupons.FindAsync(id);
            if (cupom == null) return NotFound();

            return View(cupom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cupom cupom)
        {
            if (id != cupom.Id) return BadRequest();
            if (!ModelState.IsValid) return View(cupom);

            try
            {
                _context.Update(cupom);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cupons.Any(c => c.Id == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cupom = await _context.Cupons.FindAsync(id);
            if (cupom == null) return NotFound();

            return View(cupom);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cupom = await _context.Cupons.FindAsync(id);
            if (cupom == null) return NotFound();

            _context.Cupons.Remove(cupom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public class CupomRequest
        {
            public string? Codigo { get; set; }
        }

        [HttpPost]
        [Route("/api/cupom/validar")]
        public async Task<IActionResult> ValidarCupom([FromBody] CupomRequest request)
        {
            var cupom = await _context.Cupons.FirstOrDefaultAsync(c => c.Code == request.Codigo);
            if (cupom == null)
                return NotFound(new { valido = false, mensagem = "Cupom não encontrado." });

            return Ok(new {
                valido = true,
                tipo = cupom.Type, // "percent" ou "valor"
                valor = cupom.Value,
                mensagem = "Cupom aplicado!"
            });
        }
    }
}