namespace Puissance4.OldPresentation2;

using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;

public class AuthGuard : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public ILocalStorageService LocalStorage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var token = await LocalStorage.GetItemAsStringAsync("authToken");

        if (string.IsNullOrEmpty(token))
        {
            // Redirige vers la page de login si non authentifié
            NavigationManager.NavigateTo("/login", true);
        }
    }
}
