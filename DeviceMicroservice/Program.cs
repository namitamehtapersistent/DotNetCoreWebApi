global using GraphQL.Types;

using DeviceMicroservice;
using GraphQL.Server;

var builder = WebApplication.CreateBuilder(args);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("https://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


builder.Services.AddSingleton<IDataAccess, DataAccess>();
builder.Services.AddSingleton<IDeviceProvider, DeviceProvider>();
builder.Services.AddSingleton<WeatherForcastType>();
builder.Services.AddSingleton<DeviceQuery>();
builder.Services.AddSingleton<ISchema, WeatherSchema>();
builder.Services.AddSingleton<IDeviceCreator, DeviceCreator>();
builder.Services.AddSingleton<DeviceMutation>();
builder.Services.AddSingleton<DeviceInput>();

// Configure grapgh QL in dependency injection
builder.Services.AddGraphQL(opt => opt.EnableMetrics = true).AddSystemTextJson();

var app = builder.Build();

app.UseCors("AllowReactApp");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseGraphQLAltair();
//app.UseGraphQL<ISchema>();  
app.UseGraphQL<ISchema>("/graphql");

//----------

// commented for GrapghQL
//app.UseAuthorization();
//app.MapControllers();
//----------

//app.MapGraphQL<ISchema>();

//app.UseCors("AllowMyOrigin");
//app.UseCors();

//app.MapGet("/test-cors", () => Results.Ok("CORS is working!"));


app.Run();
