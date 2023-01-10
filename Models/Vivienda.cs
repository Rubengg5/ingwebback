using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Vivienda
    {
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public Guid Id { get; set; }
        public Guid Propietario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public string Tipo { get; set; }

        public string Calle { get; set; }
        public bool ViviendaCompleta { get; set; }

        public double PrecioNoche { get; set; }

        public int MaxOcupantes { get; set; }

    }
}
