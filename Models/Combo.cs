using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace TotemPWA.Models
{
    // this is a self relation n to n
    public class Combo
    {
        public int Id { get; set; }

        public Product? ProductCombo { get; set; }
        public int ProductComboId { get; set; }
        public Product? Product { get; set; }
        public int ProductId { get; set; }
    }
}