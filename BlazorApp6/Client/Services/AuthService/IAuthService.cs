namespace BlazorApp6.Client.Services.AuthService
{
    public interface IAuthService
    {
        LogUserDTO log { get; set; }
        RegUserDTO reg { get; set; }
        Task<string> LoginAsync(LogUserDTO lg);
        Task<bool> RegisterAsync(RegUserDTO rg);
    }
}
