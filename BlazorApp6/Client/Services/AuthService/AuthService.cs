using AngleSharp.Io;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorApp6.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public LogUserDTO log {get; set ; }
        public RegUserDTO reg { get; set; }

        public AuthService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task<string> LoginAsync(LogUserDTO lg)
        {
            var res = await _httpClient.PostAsJsonAsync("api/Auth/Login", lg);
            if (!res.IsSuccessStatusCode)
            {
                return string.Empty;
            }
            return await res.Content.ReadAsStringAsync(); 
        }


        public async Task<bool> RegisterAsync(RegUserDTO rg)
        {
            var res = await _httpClient.PostAsJsonAsync("api/Auth/Register", rg);
            if (res == null)
                throw new Exception("not registered");
            return res.IsSuccessStatusCode;

        }
    }
}
