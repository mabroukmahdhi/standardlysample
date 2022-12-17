using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Standard.Api.Models.WeatherForecasts;
using Standard.Api.Models.WeatherForecasts.Exceptions;
using Standard.Api.Services.Foundations.WeatherForecasts;

namespace Standard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastsController : RESTFulController
    {
        private readonly IWeatherForecastService weatherForecastService;

        public WeatherForecastsController(IWeatherForecastService weatherForecastService) =>
            this.weatherForecastService = weatherForecastService;

        [HttpPost]
        public async ValueTask<ActionResult<WeatherForecast>> PostWeatherForecastAsync(WeatherForecast weatherForecast)
        {
            try
            {
                WeatherForecast addedWeatherForecast =
                    await this.weatherForecastService.AddWeatherForecastAsync(weatherForecast);

                return Created(addedWeatherForecast);
            }
            catch (WeatherForecastValidationException weatherForecastValidationException)
            {
                return BadRequest(weatherForecastValidationException.InnerException);
            }
            catch (WeatherForecastDependencyValidationException weatherForecastValidationException)
                when (weatherForecastValidationException.InnerException is InvalidWeatherForecastReferenceException)
            {
                return FailedDependency(weatherForecastValidationException.InnerException);
            }
            catch (WeatherForecastDependencyValidationException weatherForecastDependencyValidationException)
               when (weatherForecastDependencyValidationException.InnerException is AlreadyExistsWeatherForecastException)
            {
                return Conflict(weatherForecastDependencyValidationException.InnerException);
            }
            catch (WeatherForecastDependencyException weatherForecastDependencyException)
            {
                return InternalServerError(weatherForecastDependencyException);
            }
            catch (WeatherForecastServiceException weatherForecastServiceException)
            {
                return InternalServerError(weatherForecastServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<WeatherForecast>> GetAllWeatherForecasts()
        {
            try
            {
                IQueryable<WeatherForecast> retrievedWeatherForecasts =
                    this.weatherForecastService.RetrieveAllWeatherForecasts();

                return Ok(retrievedWeatherForecasts);
            }
            catch (WeatherForecastDependencyException weatherForecastDependencyException)
            {
                return InternalServerError(weatherForecastDependencyException);
            }
            catch (WeatherForecastServiceException weatherForecastServiceException)
            {
                return InternalServerError(weatherForecastServiceException);
            }
        }

        [HttpGet("{weatherForecastId}")]
        public async ValueTask<ActionResult<WeatherForecast>> GetWeatherForecastByIdAsync(Guid weatherForecastId)
        {
            try
            {
                WeatherForecast weatherForecast = await this.weatherForecastService.RetrieveWeatherForecastByIdAsync(weatherForecastId);

                return Ok(weatherForecast);
            }
            catch (WeatherForecastValidationException weatherForecastValidationException)
                when (weatherForecastValidationException.InnerException is NotFoundWeatherForecastException)
            {
                return NotFound(weatherForecastValidationException.InnerException);
            }
            catch (WeatherForecastValidationException weatherForecastValidationException)
            {
                return BadRequest(weatherForecastValidationException.InnerException);
            }
            catch (WeatherForecastDependencyException weatherForecastDependencyException)
            {
                return InternalServerError(weatherForecastDependencyException);
            }
            catch (WeatherForecastServiceException weatherForecastServiceException)
            {
                return InternalServerError(weatherForecastServiceException);
            }
        }
    }
}