using System.Text.Json.Serialization;

namespace TotemPWA.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        public ICollection<Combo_product> InCombo { get; set; } = new List<Combo_product>();
        public ICollection<Combo_product> Combo { get; set; } = new List<Combo_product>();
    }
}

