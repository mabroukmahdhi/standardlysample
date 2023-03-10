using Microsoft.EntityFrameworkCore;
using Standard.Api.Models.WeatherForecasts;

namespace Standard.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public async ValueTask<WeatherForecast> InsertWeatherForecastAsync(WeatherForecast weatherForecast) =>
            await InsertAsync(weatherForecast);

        public IQueryable<WeatherForecast> SelectAllWeatherForecasts() => SelectAll<WeatherForecast>();

        public async ValueTask<WeatherForecast> SelectWeatherForecastByIdAsync(Guid weatherForecastId) =>
            await SelectAsync<WeatherForecast>(weatherForecastId);

        public async ValueTask<WeatherForecast> UpdateWeatherForecastAsync(WeatherForecast weatherForecast) =>
            await UpdateAsync(weatherForecast);

        public async ValueTask<WeatherForecast> DeleteWeatherForecastAsync(WeatherForecast weatherForecast) =>
            await DeleteAsync(weatherForecast);
    }
}
