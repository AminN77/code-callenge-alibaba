
namespace CodeChallenge.ExternalServices
{
    public class WeatherAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetWeatherAsync(float longitude, float latitude)
        {
            var httpClient = _httpClientFactory.CreateClient();
            string APIURL = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m,relativehumidity_2m,windspeed_10m";

            try
            {
                var response = await httpClient.GetAsync(APIURL);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }

    }
}