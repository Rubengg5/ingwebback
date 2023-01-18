using MongoDB.Bson.Serialization.Attributes;

namespace B3serverREST.Models
{
    public class Aparcamiento
    {
        [BsonId]
        public int _id { get; set; }

        public string? nombre { get; set; }

        public string? direccion { get; set; }

        public double latitud { get; set; }

        public double longitud { get; set; }

        public int capacidad { get; set; }

        public int libres { get; set; }

        public string? correo { get; set; }


        //poiID Entero.Identificador del aparcamiento
        //nombre String. Nombre del aparcamiento
        //direccion String.Dirección del aparcamiento
        //latitud Doble.Latitud GPS de la parada.Ejemplo: 36.737835
        //longitud Doble. Longitud GPS de la parada.Ejemplo: -4.4222507
        //capacidad Entero. Número de plazas
        //libres Entero.Número de plazas libres
        //correo Email. Correo electrónico del propietario del aparcamiento



    }
}
