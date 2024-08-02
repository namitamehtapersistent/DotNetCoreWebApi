using GraphQL;

namespace DotNetCoreWebApi
{

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
            Query = serviceProvider.GetRequiredService<WeatherQuery>();  // Get Data
            Mutation = serviceProvider.GetRequiredService<WeatherForecastMutation>(); // Create, Update, Delete Data
        }
    }

   
    //-----

}
