using DeviceMicroservice.Models;
using GraphQL;

namespace DeviceMicroservice;

public class DeviceInput : InputObjectGraphType
{
    public DeviceInput()
    {
        Field<IntGraphType>("DeviceId");
        Field<IntGraphType>("Name");
        Field<StringGraphType>("Stage");
        Field<StringGraphType>("Status");
    }
}

public class DeviceMutation : ObjectGraphType
{

    public DeviceMutation(IDeviceCreator weatherForecastCreator)
    {
        Field<WeatherForcastType>("deviceCreation",
            arguments: new QueryArguments { new QueryArgument<DeviceInput> { Name = "device" } },
            resolve: x => weatherForecastCreator.Create(x.GetArgument<Device>("device"))
            );

        Field<WeatherForcastType>("deviceUpdation",
           arguments: new QueryArguments { 
               new QueryArgument<DeviceInput> { Name = "weather" },
               new QueryArgument<IntGraphType> { Name = "temperatureC" }
           },
           resolve: x => weatherForecastCreator.Update(x.GetArgument<int>("temperatureC"), x.GetArgument<Device>("device"))
           );

        Field<WeatherForcastType>("deviceDeletion",
          arguments: new QueryArguments {
               new QueryArgument<IntGraphType> { Name = "temperatureC" }
          },
          resolve: x => weatherForecastCreator.Delete(x.GetArgument<int>("temperatureC"))
          );
    }
}
