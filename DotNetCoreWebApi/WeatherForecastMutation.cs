using GraphQL;

namespace DotNetCoreWebApi;

public class WeatherForecastInput : InputObjectGraphType
{
    public WeatherForecastInput()
    {
        Field<IntGraphType>("TemperatureC");
        Field<IntGraphType>("TemperatureF");
        Field<StringGraphType>("Summary");
    }
}

public class WeatherForecastMutation : ObjectGraphType
{

    public WeatherForecastMutation(IWeatherForecastCreator weatherForecastCreator)
    {
        Field<WeatherForcastType>("weatherCreation",
            arguments: new QueryArguments { new QueryArgument<WeatherForecastInput> { Name = "weather" } },
            resolve: x => weatherForecastCreator.Create(x.GetArgument<WeatherForecast>("weather"))
            );

        Field<WeatherForcastType>("weatherUpdation",
           arguments: new QueryArguments { 
               new QueryArgument<WeatherForecastInput> { Name = "weather" },
               new QueryArgument<IntGraphType> { Name = "temperatureC" }
           },
           resolve: x => weatherForecastCreator.Update(x.GetArgument<int>("temperatureC"), x.GetArgument<WeatherForecast>("weather"))
           );

        Field<WeatherForcastType>("weatherDeletion",
          arguments: new QueryArguments {
               new QueryArgument<IntGraphType> { Name = "temperatureC" }
          },
          resolve: x => weatherForecastCreator.Delete(x.GetArgument<int>("temperatureC"))
          );
    }
}
