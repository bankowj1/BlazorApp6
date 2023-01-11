namespace BlazorApp6.Client.Services.AuthService
{
    public interface IAuthService
    {
        LogUserDTO log { get; set; }
        RegUserDTO reg { get; set; }
        Task<string> LoginAsync(string userlogin, string password);

        Task<bool> LogoutAsync();

        Task<bool> RegisterAsync(string username, string userlogin, string email, string password);
    }
}
