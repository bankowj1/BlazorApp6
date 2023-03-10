using AngleSharp.Io;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Query;
using System.Net;
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


        public async Task<string> RegisterAsync(RegUserDTO rg)
        {
            var res = await _httpClient.PostAsJsonAsync("api/Auth/Register", rg);
            if (res == null)
                return "not registered";
            if (res.StatusCode == HttpStatusCode.BadRequest)
            {
                var resp = await res.Content.ReadAsStringAsync();
                return resp;
            }
            return "";

        }
    }
}
