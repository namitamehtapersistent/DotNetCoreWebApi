global using GraphQL.Types;

using DotNetCoreWebApi;
using GraphQL.Server;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// commented for GrapghQL
//builder.Services.AddControllers();
//-------

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Added for GrapghQL
builder.Services.AddSingleton<IWeatherForecastProvider, WeatherForecastProvider>();
builder.Services.AddSingleton<WeatherForcastType>();
builder.Services.AddSingleton<WeatherQuery>();
builder.Services.AddSingleton<ISchema, WeatherSchema>();

// Configure grapgh QL in dependency injection
builder.Services.AddGraphQL(opt => opt.EnableMetrics = true).AddSystemTextJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Added for GrapghQL
app.MapGet("api/products", ([FromServices] IWeatherForecastProvider WeatherForecastProvider) =>
{
    return WeatherForecastProvider.GetWeatherForecast();
}).WithName("GetProducts");

app.UseGraphQLAltair();
app.UseGraphQL<ISchema>();  //app.UseGraphiQl("/graphql");

//----------

// commented for GrapghQL
//app.UseAuthorization();
//app.MapControllers();
//----------

app.Run();
