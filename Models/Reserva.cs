using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Reserva
    {
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public Guid Id { get; set; }
        public Guid IdVivienda { get; set; }
        public string FechaEntrada { get; set; }
        public string FechaSalida { get; set; }
        public int NPersonas { get; set; }
        public Guid Inquilino { get; set; }
        public float PrecioTotal { get; set; }
    }

}
