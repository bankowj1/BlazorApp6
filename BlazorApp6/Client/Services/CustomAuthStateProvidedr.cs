using System.Security.Claims;
using System.Text.Json;
using Blazored.SessionStorage;

namespace BlazorApp6.Client.Services
{
    public class CustomAuthStateProvidedr : AuthenticationStateProvider
    {
        private ISessionStorageService _sessionStorageService;
        public CustomAuthStateProvidedr(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;
            string token = await _sessionStorageService.GetItemAsStringAsync("token");
            if (token != null)
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
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
