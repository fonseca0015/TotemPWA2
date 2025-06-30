using TotemPWA.Models;

namespace TotemPWA.ViewModels
{
    public class ClienteViewModel
    {
        public Client Client { get; set; }

        public ClienteViewModel()
        {
            Client = new Client();
        }
    }
} 