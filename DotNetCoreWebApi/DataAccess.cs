namespace DotNetCoreWebApi;
    public interface IDataAccess {
        public IList<WeatherForecast> Get();

        public void Create(WeatherForecast wf);

        public void Update(int temperatureC, WeatherForecast wf);

        public WeatherForecast Delete(int temperatureC);
}

    public class DataAccess : IDataAccess
    {
        private readonly List<WeatherForecast> weatherForecast = new List<WeatherForecast>
        {
            new WeatherForecast
            {
                TemperatureC = 36,
                //TemperatureF = -10,

                Summary = "Test Summary for summer filter"
            },
            new WeatherForecast
            {
                TemperatureC = -10,
                //TemperatureF = -10,
                Summary = "Test Summary for winter filter"
            },
            new WeatherForecast
            {
                //TemperatureF = -10,
                TemperatureC = 3,
                Summary = "Test Summary"
            },
            new WeatherForecast
            {
                //TemperatureF = -10,
                TemperatureC = -1,
                Summary = "Test Summary"
            },
            new WeatherForecast
            {
                //TemperatureF = -10,
                TemperatureC = 6,
                Summary = "Test Summary"
            },
            new WeatherForecast
            {
                //TemperatureF = -10,
                TemperatureC = -40,
                Summary = "Test Summary"
            },
        };

        public IList<WeatherForecast> Get() => weatherForecast;

        public void Create(WeatherForecast wf)
        {
            weatherForecast.Add(wf);
        }

        public void Update(int temperatureC, WeatherForecast wf)
        {
            int index = weatherForecast.FindIndex(x => x.TemperatureC == temperatureC);
            if (index != -1)
            {
               weatherForecast[index] = wf;
            }
        }

        public WeatherForecast Delete(int temperatureC)
        {
            int index = weatherForecast.FindIndex(x => x.TemperatureC == temperatureC);
            if (index != -1)
            {
                var wf = weatherForecast[index];
                weatherForecast.RemoveAt(index);
                return wf;
            }
            return null;
        }

}

