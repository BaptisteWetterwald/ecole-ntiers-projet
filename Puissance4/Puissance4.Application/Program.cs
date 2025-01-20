using Puissance4.Application.Services;
using Puissance4.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Ajouter la configuration DataAccess
builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<AuthService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Initialiser la base de données avec des données de test
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Puissance4DbContext>();
    DbInitializer.Initialize(context);
}


app.UseHttpsRedirection();

// Middleware
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

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

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
