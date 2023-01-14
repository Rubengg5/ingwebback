using MongoDB.Bson.Serialization.Attributes;

namespace B3serverREST.Models
{
    public class Message
    {
        [BsonId]
        public Guid id { get; set; }
        public Guid de { get; set; }

        public Guid para { get; set; }

        public string? asunto { get; set; }

        public DateTime stamp { get; set; }

        public string? contenido { get; set; }

        public string? adjunto { get; set; }

    }
}
