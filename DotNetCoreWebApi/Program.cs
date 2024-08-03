global using GraphQL.Types;

using DotNetCoreWebApi;
using DotNetCoreWebApi.Models;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// commented for GrapghQL
//builder.Services.AddControllers();
//-------

// Added for EF CORE integration
//builder.Services.AddDbContext<DeviceManagementContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("GraphQLDBConnection")));
//------------

//builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowMyOrigin",
//    builder =>
//    {
//        builder.WithOrigins("https://localhost:3000") 
//     .AllowAnyHeader()
//     .AllowAnyMethod();
//    });  //("https://localhost:44456")
//});

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

// Added for EF CORE integration
//builder.Services.AddSingleton<DeviceManagementContext>();
// Added for GrapghQL
builder.Services.AddSingleton<IDataAccess, DataAccess>();
builder.Services.AddSingleton<IWeatherForecastProvider, WeatherForecastProvider>();
builder.Services.AddSingleton<WeatherForcastType>();
builder.Services.AddSingleton<WeatherQuery>();
builder.Services.AddSingleton<ISchema, WeatherSchema>();
builder.Services.AddSingleton<IWeatherForecastCreator, WeatherForecastCreator>();
builder.Services.AddSingleton<WeatherForecastMutation>();
builder.Services.AddSingleton<WeatherForecastInput>();

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



//app.UseHttpsRedirection();

// Added to call providers without graphql
//app.MapGet("api/products", ([FromServices] IWeatherForecastProvider WeatherForecastProvider) =>
//{
//    return WeatherForecastProvider.GetWeatherForecast();
//}).WithName("GetProducts");

app.UseGraphQLAltair();
app.UseGraphQL<ISchema>();  //app.UseGraphiQl("/graphql");

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
