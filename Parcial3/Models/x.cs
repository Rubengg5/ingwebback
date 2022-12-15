using MongoDB.Bson.Serialization.Attributes;

namespace Parcial3.Models
{
    public class x
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int Mg { get; set; }

    }
}
