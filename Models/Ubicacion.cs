using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.Models
{
    public class Ubicacion
    {
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public double lat { get; set; }
        public double lon { get; set; }
    }
}
