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

        public ICollection<Combo>? CombosAsCombo { get; set; }
        public ICollection<Combo>? CombosAsItem { get; set; }

        public ICollection<Promotion>? Promotions { get; set; }
        public ICollection<Additional>? Additionals { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}

