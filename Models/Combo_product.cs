using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace TotemPWA.Models
{
    // this is a self relation n to n
    public class Combo_product
    {

        public Product? product { get; set; }
        public int productId { get; set; }
        public Product? combo { get; set; }
        public int comboId { get; set; }
    }
}