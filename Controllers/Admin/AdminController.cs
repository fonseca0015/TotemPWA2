using Microsoft.AspNetCore.Mvc;

namespace TotemPWA.Controllers.Admin
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 