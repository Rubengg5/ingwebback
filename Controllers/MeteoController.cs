using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class UbicacionGeocoding
    {
        public string place_id { get; set; }
        public string licence { get; set; }
        public string osm_type { get; set; }
        public string osm_id { get; set; }
        public List<string> boundingbox { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string display_name { get; set; }
        public string @class { get; set; }
        public string type { get; set; }
        public double importance { get; set; }
        public string icon { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MeteoController : ControllerBase
    {
        [HttpGet("coordenadas/porHoras")]
        public ActionResult<string> GetPrevisionCoordenadasHourly([FromQuery] Ubicacion ubicacion)
        {
            var client = new RestClient("https://api.open-meteo.com/");
            var request = new RestRequest($"/v1/forecast?latitude={(ubicacion.lat).ToString().Replace(",", ".")}&longitude={(ubicacion.lon).ToString().Replace(",", ".")}&hourly=temperature_2m,precipitation,windspeed_10m", Method.Get);
            var response = client.Execute(request);
            if (response.Content != null)
            {
                return response.Content.ToString();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("coordenadas/diaria")]
        public ActionResult<string> GetPrevisionCoordenadasDaily([FromQuery] Ubicacion ubicacion)
        {
            var client = new RestClient("https://api.open-meteo.com/");
            var request = new RestRequest($"/v1/forecast?latitude={(ubicacion.lat).ToString().Replace(",", ".")}&longitude={(ubicacion.lon).ToString().Replace(",", ".")}&timezone=auto&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_sum", Method.Get);
            var response = client.Execute(request);
            if (response.Content != null)
            {
                return response.Content.ToString();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("texto/porHoras")]
        public ActionResult<string> GetPrevisionTextoHourly([FromQuery] string municipio)
        {
            var cliente_geocoding = new RestClient("https://eu1.locationiq.com/");
            var request_geocoding = new RestRequest($"v1/search?key=pk.2cd804ffd67b9615096d4c3069990a53&q=={municipio}&format=json");
            var response_geocoding = cliente_geocoding.Execute(request_geocoding);
            var ubicacion = JsonConvert.DeserializeObject<List<UbicacionGeocoding>>(response_geocoding.Content.ToString()).First();

            var cliente_meteo = new RestClient("https://api.open-meteo.com/");
            var request_meteo = new RestRequest($"/v1/forecast?latitude={(ubicacion.lat).ToString().Replace(",", ".")}&longitude={(ubicacion.lon).ToString().Replace(",", ".")}&hourly=temperature_2m,precipitation,windspeed_10m", Method.Get);
            var response_meteo = cliente_meteo.Execute(request_meteo);
            if (response_meteo.Content != null)
            {
                return response_meteo.Content.ToString();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("texto/diaria")]
        public ActionResult<string> GetPrevisionTextoDaily([FromQuery] string municipio)
        {
            var cliente_geocoding = new RestClient("https://eu1.locationiq.com/");
            var request_geocoding = new RestRequest($"v1/search?key=pk.2cd804ffd67b9615096d4c3069990a53&q=={municipio}&format=json");
            var response_geocoding = cliente_geocoding.Execute(request_geocoding);
            var ubicacion = JsonConvert.DeserializeObject<List<UbicacionGeocoding>>(response_geocoding.Content.ToString()).First();

            var client = new RestClient("https://api.open-meteo.com/");
            var request = new RestRequest($"/v1/forecast?latitude={(ubicacion.lat).ToString().Replace(",", ".")}&longitude={(ubicacion.lon).ToString().Replace(",", ".")}&timezone=auto&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_sum", Method.Get);
            var response = client.Execute(request);
            if (response.Content != null)
            {
                return response.Content.ToString();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
