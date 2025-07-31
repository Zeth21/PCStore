using Microsoft.JSInterop;
using PCStore.UI.Models.Commands.UserCommands;
using PCStore.UI.Models.Results.UserResults;
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
        public async Task<bool> RegisterAsync(RegisterCommand command)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/User/Account/Create", command);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Kayıt isteği başarısız. StatusCode: {response.StatusCode}");
                    return false;
                }

                var result = await response.Content.ReadFromJsonAsync<RegisterResult>();

                if (result == null)
                {
                    Console.WriteLine("Kayıt sonucu null döndü.");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(result.Token) || string.IsNullOrWhiteSpace(result.UserId))
                {
                    Console.WriteLine("Kayıt sonucu geçersiz token veya kullanıcı ID içeriyor.");
                    return false;
                }

                await SetToken(result.Token);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RegisterAsync sırasında hata oluştu: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> SendPasswordResetAsync(string email)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/User/Email/ResetPassword", new { Email = email });
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ResetPassword(string userId, string token, string newPassword)
        {
            try
            {
                var url = $"api/User/Account/ResetPassword?userId={userId}&token={token}&password={Uri.EscapeDataString(newPassword)}";
                var response = await _http.GetAsync(url);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public class LoginResult
        {
            public string Token { get; set; }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}
