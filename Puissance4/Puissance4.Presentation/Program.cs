using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Puissance4.Presentation;
using Puissance4.Presentation.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Ajouter les services pour le stockage local
builder.Services.AddBlazoredLocalStorage();

// Ajouter un service d'authentification personnalisé
builder.Services.AddScoped<AuthService>();

// Ajouter le AuthenticationStateProvider pour gérer l'état d'authentification
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

// Ajouter le service d'autorisation de base
builder.Services.AddAuthorizationCore();

// Configurer HttpClient pour appeler ton API
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7164/api/");
}).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

// Fournir le HttpClient configuré pour l'application
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

// Ajouter le gestionnaire HTTP authentifié
builder.Services.AddScoped<AuthenticatedHttpClientHandler>();

await builder.Build().RunAsync();