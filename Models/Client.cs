namespace TotemPWA.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Required]
        [CPFValidation(ErrorMessage = "O CPF deve ter exatamente 11 dígitos.")]
        public string CPF { get; set; } = string.Empty;

        public Employee? Employee { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class CPFValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string cpf)
            {
                var digits = Regex.Replace(cpf, "\\D", "");
                return digits.Length == 11;
            }
            return false;
        }
    }
}