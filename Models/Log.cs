using System.ComponentModel.DataAnnotations;

namespace B3serverREST.Models
{
    public class Log
    {
        [Required]
        public string timestamp { get; set; }
        [Required]
        public string usuario { get; set; }
        [Required]
        public string caducidad { get; set; }

        [Required]
        public string token { get; set; }

    }
}
