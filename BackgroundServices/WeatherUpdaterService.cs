using System;
using CodeChallenge.BackgroundServices;
using CodeChallenge.DAL;

namespace CodeChallenge.BackgroundServices
{
	public class WeatherUpdaterService : BackgroundService
	{
        private const int INTERVAL = 10_000;
        private readonly DataQueue _dataQueue;
        private Repository _repo;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public WeatherUpdaterService(DataQueue dataQueue, IServiceScopeFactory serviceScopeFactory)
        {
            _dataQueue = dataQueue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(true)
            {
                while (_dataQueue.Count() > 0)
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var currentWeather = _dataQueue.GetFirst();
                        _repo = scope.ServiceProvider.GetRequiredService<Repository>();

                        var weatherEntity = await _repo.GetWeatherAsync(currentWeather.Longitude, currentWeather.Latitude, isTracking: true);
                        if (weatherEntity is null)
                        {
                            await _repo.AddWeatherAsync(currentWeather);
                        }
                        else
                        {
                            weatherEntity.UpdateData(currentWeather.Data);
                        }

                        await _repo.SaveChangesAsync();
                    }
                    _dataQueue.Dequeue();
                }

                await Task.Delay(INTERVAL);
            }
        }
    }
}

