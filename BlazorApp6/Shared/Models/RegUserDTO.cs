using Ganss.Xss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp6.Shared.Models
{
    public partial class RegUserDTO
    {
        public string Username { get; set; } = null!;
        public string Userlogin { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public string Email { get; set; } = null!;

        public void SanitizeInput()
        {
            HtmlSanitizer _sanitizer = new HtmlSanitizer();
            this.Userlogin = _sanitizer.Sanitize(this.Userlogin);
            this.Username = _sanitizer.Sanitize(this.Username);
            this.Email = _sanitizer.Sanitize(this.Email);
            this.Pass = _sanitizer.Sanitize(this.Pass);
        }
    }
}
