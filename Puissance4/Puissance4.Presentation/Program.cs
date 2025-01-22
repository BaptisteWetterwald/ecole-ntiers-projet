using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Puissance4.Presentation;
using Blazored.LocalStorage;
using Puissance4.Presentation.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Ajouter le stockage local pour gérer les tokens JWT
builder.Services.AddBlazoredLocalStorage();

// Ajouter le gestionnaire HTTP authentifié
builder.Services.AddScoped<AuthenticatedHttpClientHandler>();

// Ajouter le service d'authentification
builder.Services.AddScoped<AuthService>();

// Configurer le HttpClient pour l'API
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7164/api/"); // URL de l'API
}).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

// Configurer un HttpClient par défaut pour d'autres requêtes
builder.Services.AddScoped(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return clientFactory.CreateClient("API"); // Utilise la configuration de l'API
});

await builder.Build().RunAsync();