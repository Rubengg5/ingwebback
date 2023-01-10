
using RestSharp;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class MeteoService
    {
        /*public static string GetPrevision(Ubicacion ubicacion)
        {
            var client = new RestClient("https://api.open-meteo.com/");
            var request = new RestRequest($"/v1/forecast?latitude={ubicacion.lat}&longitude={ubicacion.lon}&hourly=temperature_2m,precipitation,windspeed_10m", Method.Get);
            var response = client.Execute(request);
            if (response.Content != null)
            {
                return response.Content;
            }
            

        }*/
    }
}
