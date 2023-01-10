using WebAPI.Models;

namespace B3serverREST.Models
{
    public class Mensaje
    {
        public Guid Id { get; set; }
        public Guid remitente { get; set; }

        public string nombreRemitente { get; set; }

        public Guid destinatario { get; set; }

        public string mensaje { get; set; }

        public DateTime timestamp { get; set; }
    }
}
