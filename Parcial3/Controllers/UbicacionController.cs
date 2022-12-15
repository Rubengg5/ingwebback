using Microsoft.AspNetCore.Mvc;
using Parcial3.Models;
using RestSharp;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.OpenApi.Any;

namespace Parcial3.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UbicacionController : ControllerBase
    {

        [HttpGet("/forward/{calle}")]
        public async Task<ActionResult<Ubicacion>> GetFromCalle(string calle)
        {
            var cliente_geocoding = new RestClient("https://eu1.locationiq.com/");
            var request_geocoding = new RestRequest($"v1/search?key=pk.2cd804ffd67b9615096d4c3069990a53&q=={calle}&format=json");
            var response_geocoding = cliente_geocoding.Execute(request_geocoding);
            var ubicacion = JsonConvert.DeserializeObject<List<UbicacionGeocodingForward>>(response_geocoding.Content.ToString()).First();
            Console.WriteLine(ubicacion.lat);
            var r = new Ubicacion();
            r.latitud = (float)Double.Parse(ubicacion.lat, CultureInfo.InvariantCulture);
            Console.WriteLine(r.latitud);
            r.longitud = (float)Double.Parse(ubicacion.lon, CultureInfo.InvariantCulture);
            return r;
        }
        [HttpGet("/reverse/{lat}/{lon}")]
        public async Task<ActionResult<string>> GetFromCoord(double lat, double lon)
        {
            String latitud= lat.ToString(CultureInfo.InvariantCulture);
            String longitud = lon.ToString(CultureInfo.InvariantCulture);
            var cliente_geocoding = new RestClient("https://eu1.locationiq.com/");
            var request_geocoding = new RestRequest($"v1/reverse?key=pk.2cd804ffd67b9615096d4c3069990a53&lat={latitud}&lon={longitud}&format=json");
            var response_geocoding = cliente_geocoding.Execute(request_geocoding);
            var data = (JObject)JsonConvert.DeserializeObject(response_geocoding.Content.ToString());
            Console.WriteLine(data.ToString());
            return data.ToString();
        }
    }
}
