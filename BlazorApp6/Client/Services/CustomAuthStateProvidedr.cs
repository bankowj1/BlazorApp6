using System.Security.Claims;

namespace BlazorApp6.Client.Services
{
    public class CustomAuthStateProvidedr : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsImV4cCI6MTY3MzY0MTIxMn0.0B5JrG5qbPjHKuUjEVLVz2mcppBaeLvSuJyFfVvv73HZuVa7Q3n_g3ZJELKHHyWHVZLQA3eItXETmKVPXEtW4w";

            var identity = new ClaimsIdentity();

            var us = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(us);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }
    }
}
