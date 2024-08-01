using GraphQL;
using GraphQL.Types;

namespace DotNetCoreWebApi
{
    public class WeatherForecast
    {
        //public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    // Added for GrapghQL

    //1. Type for graphql
    public class WeatherForcastType : ObjectGraphType<WeatherForecast>
    {
        public WeatherForcastType()
        {
            Field(x => x.TemperatureC);
            Field(x => x.TemperatureF);
            Field(x => x.Summary);

        }
    }

    // 2. Query for graphql, is actually going to give data
    public class WeatherQuery : ObjectGraphType
    {
        public WeatherQuery(IWeatherForecastProvider weatherForecastProvider)
        {
            Field<ListGraphType<WeatherForcastType>>(Name = "weathers", resolve: x => weatherForecastProvider.GetWeatherForecast());
            Field<WeatherForcastType>(Name = "weather",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "TemperatureC" }),
                resolve: x => weatherForecastProvider.GetWeatherForecast().FirstOrDefault(p => p.TemperatureC == x.GetArgument<int>("TemperatureC"))
                );
        }
    }

    // 3. Define Schema : Used for documentation
    public class WeatherSchema : Schema
    {
        public WeatherSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<WeatherQuery>();
        }
    }

    public interface IWeatherForecastProvider
    {
        WeatherForecast[] GetWeatherForecast();
    }
    public class WeatherForecastProvider : IWeatherForecastProvider
    {
        public WeatherForecast[] GetWeatherForecast()
        {

            WeatherForecast[] result = new WeatherForecast[6];
            result[0] = new WeatherForecast
            {
                TemperatureC = 36,
                Summary = "Test Summary for summer filter"
            };
            result[1] = new WeatherForecast
            {
                TemperatureC = -10,
                Summary = "Test Summary for winter filter"
            };
            for (int i = 2; i < 6; i++)
            {
                result[i] = new WeatherForecast
                {
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = "Test Summary"
                };
            }

            return result;
        }
    }
    //-----

}
