namespace BlazorApp6.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LogUserDTO lg);
        Task<string> RegisterAsync(RegUserDTO rg);
    }
}
