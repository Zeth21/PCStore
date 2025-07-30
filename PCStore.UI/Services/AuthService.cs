using Microsoft.JSInterop;
using PCStore.UI.Models.Commands.UserCommands;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace PCStore.UI.Services
{
    public class AuthService
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient _http;

        public event Action OnChange;

        public AuthService(IJSRuntime js, HttpClient http)
        {
            _js = js;
            _http = http;
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "jwt_token");
            return !string.IsNullOrEmpty(token);
        }

        public async Task SetToken(string token)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", "jwt_token", token);
            NotifyStateChanged();
        }

        public async Task RemoveToken()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "jwt_token");
            NotifyStateChanged();
        }

        public async Task<bool> LoginAsync(LoginCommand command)
        {
            var response = await _http.PostAsJsonAsync("api/User", command);

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResult>();
            if (result is null || string.IsNullOrEmpty(result.Token))
                return false;

            await SetToken(result.Token);
            return true;
        }

        public class LoginResult
        {
            public string Token { get; set; }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}
