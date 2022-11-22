using System.Threading;
using CodeChallenge.BackgroundServices;
using CodeChallenge.DAL;
using CodeChallenge.DTOs;
using CodeChallenge.ExternalServices;

namespace CodeChallenge.Logic
{
    public class WeatherLogicManager
    {
        private const int API_TIMEOUT = 4_000;
        private const int QUERY_TIMEOUT = 500;
        private readonly Repository _repo;
        private readonly WeatherAPIService _apiService;
        private readonly DataQueue _dataQueue;
        public WeatherLogicManager(Repository repo, WeatherAPIService apiService, DataQueue dataQueue)
        {
            _repo = repo;
            _apiService = apiService;
            _dataQueue = dataQueue;
        }

        public async Task<ResponseDto> GetWeatherAsync(float longitude, float latitude)
        {
            var cachedWeatherEntityTask = GetCachedWeatherAsync(longitude, latitude);
            var weatherFromAPITask = GetWeatherFromAPIAsync(longitude, latitude);
            var messages = new List<string>();

            if (await Task.WhenAny(weatherFromAPITask, Task.Delay(API_TIMEOUT)) == weatherFromAPITask)
            {
                if (!string.IsNullOrEmpty(weatherFromAPITask.Result))
                {
                    var weatherEntity = new Weather(longitude, latitude, weatherFromAPITask.Result);
                    _dataQueue.Enqueue(weatherEntity);
                    messages.Add("API call");
                    return new ResponseDto(weatherEntity, 200, messages);
                }
            }

            if (await Task.WhenAny(cachedWeatherEntityTask, Task.Delay(QUERY_TIMEOUT)) == cachedWeatherEntityTask)
            {
                var weatherEntity = cachedWeatherEntityTask.Result;
                if (weatherEntity is not null)
                {
                    messages.Add("Database");
                    return new ResponseDto(weatherEntity, 200, messages);
                }
            }

            messages.Add("Data is not available for now. try again later.");
            return new ResponseDto(null, 503, messages);
        }

        #region privates

        private async Task<Weather> GetCachedWeatherAsync(float longitude, float latitude)
        {
            return await _repo.GetWeatherAsync(longitude, latitude);
        }

        private async Task<string> GetWeatherFromAPIAsync(float longitude, float latitude)
        {
            return await _apiService.GetWeatherAsync(longitude, latitude);
        }

        #endregion

    }
}