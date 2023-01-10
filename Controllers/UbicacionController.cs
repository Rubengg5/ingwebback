using WebAPI.Models;
using WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using System.Globalization;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UbicacionController : ControllerBase
    {

        [HttpGet("{calle}")]
        public async Task<ActionResult<Ubicacion>> Get(string calle)
        {
            var cliente_geocoding = new RestClient("https://eu1.locationiq.com/");
            var request_geocoding = new RestRequest($"v1/search?key=pk.2cd804ffd67b9615096d4c3069990a53&q=={calle}&format=json");
            var response_geocoding = cliente_geocoding.Execute(request_geocoding);
            var ubicacion = JsonConvert.DeserializeObject<List<UbicacionGeocoding>>(response_geocoding.Content.ToString()).First();
            Console.WriteLine(ubicacion.lat);
            var r = new Ubicacion();
            r.lat = Double.Parse(ubicacion.lat, CultureInfo.InvariantCulture);
            Console.WriteLine(r.lat);
            r.lon = Double.Parse(ubicacion.lon, CultureInfo.InvariantCulture);
            return r;
        }
    }
}
