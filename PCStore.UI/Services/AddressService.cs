using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PCStore.UI.Models.Commands.AddressCommands;
using PCStore.UI.Models.Results.AddressResults;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace PCStore.UI.Services
{
    public class AddressService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        public AddressService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
        }
        public async Task<List<GetAllAddressesResult>?> GetAllAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt_token");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else if (string.IsNullOrWhiteSpace(token))
            {
                _navigationManager.NavigateTo($"/login?returnUrl={Uri.EscapeDataString("/cart")}", forceLoad: true);
                return null;
            }
            var response = await _httpClient.GetAsync("api/Address");
            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<List<GetAllAddressesResult>>();
            return result;
        }

        public async Task<bool> DeleteAddress(int addressId) 
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt_token");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else if (string.IsNullOrWhiteSpace(token))
            {
                _navigationManager.NavigateTo($"/login?returnUrl={Uri.EscapeDataString("/cart")}", forceLoad: true);
                return false;
            }
            var response = await _httpClient.DeleteAsync($"api/Address/{addressId}");
            if (!response.IsSuccessStatusCode)
                return false;
            return true;
        }

        public async Task<GetAllAddressesResult> CreateAddress(CreateAddressCommand request)
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt_token");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else if (string.IsNullOrWhiteSpace(token))
            {
                _navigationManager.NavigateTo($"/login?returnUrl={Uri.EscapeDataString("/cart")}", forceLoad: true);
                return null;
            }
            var response = await _httpClient.PostAsJsonAsync($"api/Address", request);
            if (!response.IsSuccessStatusCode)
                return null;
            var result = await response.Content.ReadFromJsonAsync<GetAllAddressesResult>();
            return result;
        }
    }

}
