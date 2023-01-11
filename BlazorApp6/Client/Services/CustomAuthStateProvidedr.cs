namespace BlazorApp6.Client.Services
{
    public class CustomAuthStateProvidedr : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
