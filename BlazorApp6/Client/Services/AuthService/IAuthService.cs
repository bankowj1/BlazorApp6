namespace BlazorApp6.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LogUserDTO lg);
        Task<bool> RegisterAsync(RegUserDTO rg);
    }
}
