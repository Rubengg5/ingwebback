using MongoDB.Bson.Serialization.Attributes;

namespace Parcial3.Models
{
    public class Parada
    {
        [BsonId]
        public int codLinea { get; set; }
        public string nombreLinea { get; set; }
        public int sentido { get; set; }
        public int orden { get; set; }
        public int codParada { get; set; }
        public string nombreParada { get; set; }
        public string direccion { get; set; }
        public float lon { get; set; }
        public float lat { get; set; }
    }
}
