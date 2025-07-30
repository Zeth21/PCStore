using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PCStore.UI.Models.Commands.ShopCartCommands;
using PCStore.UI.Models.Results.CartResults;
using System.Net.Http.Json;

public class CartService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly NavigationManager _navigationManager;
    public CartService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
        _navigationManager = navigationManager;
    }

    public async Task<string> CreateShopCartItem(CreateShopCartItemCommand command)
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

        var response = await _httpClient.PostAsJsonAsync("api/ShoppingCart", command);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        else
        {
            return $"İstek başarısız oldu. Durum kodu: {(int)response.StatusCode}";
        }
    }
    public async Task<BulkGetShopCartItemsResult?> GetShopCartItemsAsync(int? couponId = null, CancellationToken cancellationToken = default)
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

        var url = "api/ShoppingCart";
        if (couponId.HasValue)
            url += $"?couponId={couponId.Value}";

        var response = await _httpClient.GetAsync(url, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<BulkGetShopCartItemsResult>(cancellationToken: cancellationToken);
        return result;
    }

    public async Task<string?> RemoveShopCartItemAsync(int id, CancellationToken cancellationToken = default)
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt_token");

        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
        else if (string.IsNullOrWhiteSpace(token))
        {
            _navigationManager.NavigateTo($"/login?returnUrl=/cart", forceLoad: true);
            return null;
        }

        var url = $"api/ShoppingCart/{id}";

        var response = await _httpClient.DeleteAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var result = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
        return result;
    }

}
