using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PCStore.Application.Services.UserService.ServiceDTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly NavigationManager _navigationManager;
    public UserService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
        _navigationManager = navigationManager;
    }


    public async Task<ServiceGetUserProfileResult?> GetUserProfileAsync()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt_token");

        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
        else if (string.IsNullOrWhiteSpace(token))
        {
            // Kullanıcıyı login sayfasına yönlendir
            _navigationManager.NavigateTo($"/login?returnUrl=/cart", forceLoad: true);
            return null;
        }

        var response = await _httpClient.GetAsync("api/User");

        if (!response.IsSuccessStatusCode)
        {
            // Loglama / kullanıcıya hata gösterme yapılabilir
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<ServiceGetUserProfileResult>();

        return result;
    }
}
