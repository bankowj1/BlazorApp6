using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.SessionStorage;

namespace BlazorApp6.Client.Services
{
    public class CustomAuthStateProvidedr : AuthenticationStateProvider
    {
        private ISessionStorageService _sessionStorageService;
        private readonly HttpClient _httpClient;

        public CustomAuthStateProvidedr(ISessionStorageService sessionStorageService, HttpClient httpClient)
        {
            _sessionStorageService = sessionStorageService;
            _httpClient = httpClient;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            ClaimsIdentity identity;
            string token = await _sessionStorageService.GetItemAsStringAsync("token");
            if (token != null )
            {
                if (token == string.Empty)
                {
                    throw new ArgumentException("empty token no authorization for u");
                }
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer",token.Replace("\"",""));
            }
            else
            {
                identity = new ClaimsIdentity();
            }
            var us = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(us);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
        public async void MarkUserAsNotAuthenticated()
        {
            await _sessionStorageService.RemoveItemAsync("token");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        }
    }
}
