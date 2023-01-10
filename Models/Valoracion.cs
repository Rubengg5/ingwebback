using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Valoracion
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid Autor { get; set; }
        public Guid Vivienda { get; set; }
        public string Descripcion { get; set; }
        public int Puntuacion { get; set; }
    }
}
