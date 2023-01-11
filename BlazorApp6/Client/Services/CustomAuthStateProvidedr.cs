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
            string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsImV4cCI6MTY3MzY0MTIxMn0.0B5JrG5qbPjHKuUjEVLVz2mcppBaeLvSuJyFfVvv73HZuVa7Q3n_g3ZJELKHHyWHVZLQA3eItXETmKVPXEtW4w";

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

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
        }
    }
}
