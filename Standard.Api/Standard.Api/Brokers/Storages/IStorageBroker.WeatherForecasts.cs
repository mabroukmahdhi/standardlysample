using System;
using System.Linq;
using System.Threading.Tasks;
using Standard.Api.Models.WeatherForecasts;

namespace Standard.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<WeatherForecast> InsertWeatherForecastAsync(WeatherForecast weatherForecast);
        IQueryable<WeatherForecast> SelectAllWeatherForecasts();
        ValueTask<WeatherForecast> SelectWeatherForecastByIdAsync(Guid weatherForecastId);
    }
}
