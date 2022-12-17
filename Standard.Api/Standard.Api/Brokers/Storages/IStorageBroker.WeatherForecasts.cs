using Standard.Api.Models.WeatherForecasts;

namespace Standard.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<WeatherForecast> InsertWeatherForecastAsync(WeatherForecast weatherForecast);
        IQueryable<WeatherForecast> SelectAllWeatherForecasts();
        ValueTask<WeatherForecast> SelectWeatherForecastByIdAsync(Guid weatherForecastId);
        ValueTask<WeatherForecast> UpdateWeatherForecastAsync(WeatherForecast weatherForecast);
        ValueTask<WeatherForecast> DeleteWeatherForecastAsync(WeatherForecast weatherForecast);
    }
}
