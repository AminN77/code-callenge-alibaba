using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.DAL
{
    public class Repository
    {
        private readonly PgSqlDbContext _context;
        public Repository(PgSqlDbContext context)
        {
            _context = context;
            _context.Database.Migrate();
        }

        public async Task<Weather> GetWeatherAsync(float longitude, float latitude, bool isTracking = false)
        {
            return isTracking ?
                await _context.Weathers.Where(x => x.Longitude.Equals(longitude) && x.Latitude.Equals(latitude))
                  .OrderByDescending(x => x.LastUpdateDateTime).FirstOrDefaultAsync() :
                await _context.Weathers.Where(x => x.Longitude.Equals(longitude) && x.Latitude.Equals(latitude))
                  .OrderByDescending(x => x.LastUpdateDateTime).AsNoTracking().FirstOrDefaultAsync();


        }

        public async Task AddWeatherAsync(Weather weather)
        {
            await _context.AddAsync<Weather>(weather);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}