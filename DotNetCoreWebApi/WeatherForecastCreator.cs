namespace DotNetCoreWebApi;

public interface IWeatherForecastCreator
{
    public WeatherForecast Create(WeatherForecast wf);

    public WeatherForecast Update(int temperatureC, WeatherForecast wf);

    public WeatherForecast Delete(int temperatureC);
}

public class WeatherForecastCreator : IWeatherForecastCreator
{
    private readonly IDataAccess dataAccess;
     
      public WeatherForecastCreator(IDataAccess dataAccess)
      {
        this.dataAccess = dataAccess;
      }

    public WeatherForecast Create(WeatherForecast wf)
    {
        dataAccess.Create(wf);
        return wf;
    }

    public WeatherForecast Update(int temperatureC, WeatherForecast wf)
    {
        dataAccess.Update(temperatureC, wf);
        return wf;
    }

    public WeatherForecast Delete(int temperatureC)
    {
        return dataAccess.Delete(temperatureC);
    }
}

