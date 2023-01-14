using WebAPI.Models;

namespace B3serverREST.Models
{
    public class MessageDTO
    {
        public Guid id { get; set; }
        public Guid de { get; set; }

        public Guid para { get; set; }

        public string? asunto { get; set; }

        public DateTime stamp { get; set; }
    }
}
