using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Blaze4.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Modif base url for HttpClient (gpt)
builder.Services.AddScoped(sp=> new HttpClient { BaseAddress = new Uri("https://localhost:5001/api/") });
// End Modif base url for HttpClient (gpt)

// Modif for CORS (gpt)
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:5002")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// End Modif for CORS (gpt)

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Modif to register GameService (gpt)
builder.Services.AddScoped<GameService>(); // Enregistre GameService
// End Modif to register GameService (gpt)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Modif for CORS (gpt)
app.UseCors("CorsPolicy");
// End Modif for CORS (gpt)

// app.MapHub<GameHub>("/gamehub");

// Modif to serve static files (gpt)
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,  // Cela permet de servir des fichiers inconnus (comme .dat)
    DefaultContentType = "application/octet-stream"  // Définit un type MIME générique
});
app.MapFallbackToFile("index.html");
// End Modif to serve static files (gpt)

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");


// Modif to add GameService (gpt)
app.MapGet("/games", (GameService service) => service.GetGamesAwaitingGuest())
    .WithName("GetGamesAwaitingGuest");
// End Modif to add GameService (gpt)

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
