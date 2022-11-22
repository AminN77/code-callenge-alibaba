namespace CodeChallenge.DAL
{
    public class Weather
    {
        public Guid Id { get; init; }
        public float Latitude { get; init; }
        public float Longitude { get; init; }
        public string? Data { get; private set; }
        public DateTime LastUpdateDateTime{ get; private set; }

        public Weather(float longitude, float latitude, string data)
        {
            Id = Guid.NewGuid();
            Latitude = latitude;
            Longitude = longitude;
            Data = data;
            LastUpdateDateTime = DateTime.UtcNow;
        }

        public void UpdateData(string newData)
        {
            Data = newData;
            LastUpdateDateTime = DateTime.UtcNow;
        }
    }
}