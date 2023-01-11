using System.ComponentModel.DataAnnotations;

namespace BlazorApp6.ViewModels
{
    public class UserViewModel
    {
        [StringLength(64, MinimumLength = 8, ErrorMessage = "The name must be between 8 and 64 characters long.")]
        public string Username { get; set; } = null!;
        [StringLength(64, MinimumLength = 8, ErrorMessage = "The name must be between 8 and 64 characters long.")]
        public string Userlogin { get; set; } = null!;
        [StringLength(64, MinimumLength = 8, ErrorMessage = "The name must be between 8 and 64 characters long.")]
        public string Email { get; set; } = null!;
        [StringLength(64, MinimumLength = 8, ErrorMessage = "The name must be between 8 and 64 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
            ErrorMessage = "Password must have at least one uppercase letter, one number, and one special character")]
        public string Password { get; set; } = null!;

        public static implicit operator UserViewModel(RegUserDTO user)
        {
            return new UserViewModel
            {
                Username = user.Username,
                Userlogin = user.Userlogin,
                Email = user.Email,
                Password = user.Pass
            };
        }

        public static implicit operator RegUserDTO(UserViewModel user)
        {
            return new RegUserDTO
            {
                Username = user.Username,
                Userlogin = user.Userlogin,
                Email = user.Email,
                Pass = user.Password
            };
        }

        public static implicit operator LogUserDTO(UserViewModel user)
        {
            return new LogUserDTO
            {
                Userlogin = user.Userlogin,
                Pass = user.Password
            };
        }
    }
}