using GraphQL;
using DeviceMicroservice.Models;

namespace DeviceMicroservice
{

    // Added for GrapghQL

    //1. Type for graphql
    public class WeatherForcastType : ObjectGraphType<Device>
    {
        public WeatherForcastType()
        {
            Field(x => x.DeviceId);
            Field(x => x.Name);
            Field(x => x.Stage);
            Field(x => x.Status);

        }
    }

    // 2. Query for graphql, is actually going to give data
    public class DeviceQuery : ObjectGraphType
    {
        public DeviceQuery(IDeviceProvider weatherForecastProvider)
        {
            Field<ListGraphType<WeatherForcastType>>(Name = "devices", resolve: x => weatherForecastProvider.GetDevice());
            Field<WeatherForcastType>(Name = "device",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "TemperatureC" }),
                resolve: x => weatherForecastProvider.GetDevice().FirstOrDefault(p => p.DeviceId == x.GetArgument<int>("DeviceId"))
                );
        }
    }

    // 3. Define Schema : Used for documentation
    public class WeatherSchema : Schema
    {
        public WeatherSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<DeviceQuery>();  // Get Data
            Mutation = serviceProvider.GetRequiredService<DeviceMutation>(); // Create, Update, Delete Data
        }
    }

   
    //-----

}
