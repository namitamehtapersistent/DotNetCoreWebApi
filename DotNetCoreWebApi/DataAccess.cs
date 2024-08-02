using DotNetCoreWebApi.Models;

namespace DotNetCoreWebApi;
public interface IDataAccess
{
    public IList<WeatherForecast> Get();

    public void Create(WeatherForecast wf);

    public void Update(int temperatureC, WeatherForecast wf);

    public WeatherForecast Delete(int temperatureC);
}

public class DataAccess : IDataAccess
{
    private readonly DeviceManagementContext _context;

    public DataAccess()
    {
        this._context = new DeviceManagementContext();
    }

    public IList<WeatherForecast> Get() => this._context.WeatherForecasts.ToList();

    public void Create(WeatherForecast wf)
    {
        this._context.WeatherForecasts.Add(wf);
        this._context.SaveChanges();
    }

    public void Update(int temperatureC, WeatherForecast wf)
    {
        var deviceToUpdate = this._context.WeatherForecasts.FirstOrDefault(o => o.TemperatureC == temperatureC);
        if (deviceToUpdate != null)
        {
            this._context.Entry(deviceToUpdate).CurrentValues.SetValues(wf);
            this._context.SaveChanges();
        }
    }

    public WeatherForecast Delete(int temperatureC)
    {
        var weather = this._context.WeatherForecasts.FirstOrDefault(x => x.TemperatureC == temperatureC);
        if (weather != null)
        {
            this._context.WeatherForecasts.Remove(weather);
            this._context.SaveChanges();
            return weather;
        }
        return null;

    }

}

