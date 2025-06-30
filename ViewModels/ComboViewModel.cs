using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TotemPWA.Models;

namespace TotemPWA.ViewModels
{
    public class ComboViewModel
    {
        public Combo Combo { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }

        public ComboViewModel()
        {
            Combo = new Combo();
            Products = new List<SelectListItem>();
        }
    }
}
