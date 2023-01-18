using MongoDB.Bson.Serialization.Attributes;

namespace B3serverREST.Models
{
    public class Imagen
    {
        [BsonId]
        public int _id { get; set; }

        public int idAparcamiento { get; set; }

        public string? imagen { get; set; }

    }
}
