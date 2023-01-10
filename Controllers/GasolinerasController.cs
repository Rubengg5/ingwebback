using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using WebAPI.Models;
using WebAPI.Services;
namespace WebAPI.Controllers
{
    public class Gasolinera
    {
        public string Provincia { get; set; }
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public int Código_po { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public double Precio_gas { get; set; }
        public double Precio_g_3 { get; set; }
        public double Precio_g_5 { get; set; }
        public double Precio_g_6 { get; set; }
        public string Horario { get; set; }
        public string Dirección { get; set; }
    }

    public class Feature
    {
        public Gasolinera attributes { get; set; }
        public Geometry geometry { get; set; }
    }

    public class Field
    {
        public string name { get; set; }
        public string type { get; set; }
        public string alias { get; set; }
        public string sqlType { get; set; }
        public int length { get; set; }
        public object domain { get; set; }
        public object defaultValue { get; set; }
    }

    public class Geometry
    {
        public double x { get; set; }
        public double y { get; set; }
    }

    public class Root
    {
        public string objectIdFieldName { get; set; }
        public UniqueIdField uniqueIdField { get; set; }
        public string globalIdFieldName { get; set; }
        public string geometryType { get; set; }
        public SpatialReference spatialReference { get; set; }
        public List<Field> fields { get; set; }
        public List<Feature> features { get; set; }
    }

    public class SpatialReference
    {
        public int wkid { get; set; }
        public int latestWkid { get; set; }
    }

    public class UniqueIdField
    {
        public string name { get; set; }
        public bool isSystemMaintained { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class GasolinerasController : ControllerBase
    {
        [HttpGet("municipio")]
        public ActionResult<Feature> GetGasolineraByMunicipio([FromQuery] string municipio)
        {
            var client = new RestClient("https://services1.arcgis.com/");
            var request = new RestRequest($"nCKYwcSONQTkPA4K/arcgis/rest/services/Gasolineras_Pro/FeatureServer/0/query?where=Municipio%20%3D%20'{municipio.ToUpper()}'&outFields=Provincia,Municipio,Localidad,Código_po,Longitud,Latitud,Precio_gas,Precio_g_3,Precio_g_5,Precio_g_6,Horario,Dirección&outSR=4326&f=json", Method.Get);
            var response = client.Execute(request);
            Root respuesta = JsonConvert.DeserializeObject<Root>(response.Content.ToString());
            Feature gasolinera = respuesta.features[0];
            if (gasolinera != null)
            {
                return gasolinera;
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("provincia")]
        public ActionResult<Feature> GetGasolineraByCoordenada([FromQuery] string provincia)
        {
            var client = new RestClient("https://services1.arcgis.com/");
            var request = new RestRequest($"nCKYwcSONQTkPA4K/arcgis/rest/services/Gasolineras_Pro/FeatureServer/0/query?where=Provincia%20%3D%20'{provincia.ToUpper()}'&outFields=Provincia,Municipio,Localidad,Código_po,Longitud,Latitud,Precio_gas,Precio_g_3,Precio_g_5,Precio_g_6,Horario,Dirección&outSR=4326&f=json", Method.Get);
            var response = client.Execute(request);
            Root respuesta = JsonConvert.DeserializeObject<Root>(response.Content.ToString());
            Feature gasolinera = respuesta.features[0];
            if (gasolinera != null)
            {
                return gasolinera;
            }
            else
            {
                return NotFound();
            }
        }
    }
}
