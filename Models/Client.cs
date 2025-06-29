namespace TotemPWA.Models
{

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;

        public Employee? Employee { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}