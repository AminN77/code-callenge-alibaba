using CodeChallenge.DAL;

namespace CodeChallenge.BackgroundServices
{
    public class DataQueue
    {
        private const int MEMORY_LIMIT = 100;
        private readonly List<Weather> _weathers = new List<Weather>();

        public void Enqueue(Weather weather)
        {
            var sameWeather = _weathers.Where
                (x => x.Latitude.Equals(weather.Latitude) && x.Longitude.Equals(weather.Longitude));
            foreach (var item in sameWeather)
            {
                DequeueWithParam(item);
            }

            if (Count() >= MEMORY_LIMIT)
            {
                Dequeue();
            }

            _weathers.Add(weather);
        }

        public int Count()
            => _weathers.Count;

        public void Dequeue()
        {
            _weathers.Remove(_weathers.FirstOrDefault());
        }

        public Weather GetFirst()
        {
            return _weathers.FirstOrDefault();
        }

        private void DequeueWithParam(Weather weather)
        {
            _weathers.Remove(weather);
        }

    }
}