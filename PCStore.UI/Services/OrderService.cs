using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PCStore.UI.Models.Commands.OrderCommands;
using PCStore.UI.Models.Results.OrderResults;
using System.Net.Http.Json;

namespace PCStore.UI.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        public OrderService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
        }

        public async Task<ServiceCreateOrderResult?> CreateOrderAsync(ServiceCreateOrderCommand request)
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

            var response = await _httpClient.PostAsJsonAsync("api/Order", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ServiceCreateOrderResult>();
            }

            return null;
        }

        public async Task<ServiceGetOrderDetailsByOrderIdResult?> GetOrderDetailsByIdAsync(int orderId)
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
            var response = await _httpClient.GetAsync($"api/Order/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceGetOrderDetailsByOrderIdResult>();
                return result;
            }

            return null;
        }

        public async Task<List<ListGetOrderStatusByOrderIdResult>?> GetOrderStatusListByOrderIdAsync(int orderId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Order/{orderId}/status");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<ListGetOrderStatusByOrderIdResult>>();
                    return result;
                }
                else
                {
                    Console.WriteLine("Başka durum bulunamadı!");
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Hata! " + ex.Message);
                return null;
            }
        }

        public async Task<List<UserGetOrderListResult>> GetUserOrdersAsync()
        {
            try
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
                var response = await _httpClient.GetAsync("api/Order");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<UserGetOrderListResult>>();
                    return result ?? new();
                }
                else
                {
                    // Gerekirse loglama yapılabilir
                    return new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sipariş listesi alınırken hata oluştu: {ex.Message}");
                return new();
            }
        }

    }

}
