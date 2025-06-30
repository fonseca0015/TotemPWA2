using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TotemPWA.Models;

namespace TotemPWA.ViewModels
{
    public class AditionalViewModel
    {
        public Additional Additional { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
        public IEnumerable<SelectListItem> Ingredients { get; set; }

        public AditionalViewModel()
        {
            Additional = new Additional();
            Products = new List<SelectListItem>();
            Ingredients = new List<SelectListItem>();
        }
    }
} 