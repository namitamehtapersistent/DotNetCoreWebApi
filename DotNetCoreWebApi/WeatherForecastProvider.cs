namespace DotNetCoreWebApi;

    public interface IWeatherForecastProvider
    {
        WeatherForecast[] GetWeatherForecast();
    }
    public class WeatherForecastProvider : IWeatherForecastProvider
    {
       private readonly IDataAccess dataAccess;

    public WeatherForecastProvider(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    public WeatherForecast[] GetWeatherForecast() => dataAccess.Get().ToArray();
     
    }

