namespace BlazorApp6.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; } = null!;
        public string Userlogin { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public static implicit operator UserViewModel(User user)
        {
            return new UserViewModel
            {
                Username = user.Username,
                Userlogin = System.Text.Encoding.UTF8.GetString(user.Userlogin),
                Email = System.Text.Encoding.UTF8.GetString(user.Email),
                Password = System.Text.Encoding.UTF8.GetString(user.Pass)
            };
        }

        public static implicit operator User(UserViewModel user)
        {
            return new User
            {
                Username = user.Username,
                Userlogin = System.Text.Encoding.UTF8.GetBytes(user.Userlogin),
                Email = System.Text.Encoding.UTF8.GetBytes(user.Email),
                Pass = System.Text.Encoding.UTF8.GetBytes(user.Password)
            };
        }
    }
}