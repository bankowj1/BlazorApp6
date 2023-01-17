using Ganss.Xss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp6.Shared.Models
{
    public partial class LogUserDTO
    {
        public string Userlogin { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public void SanitizeInput()
        {
            HtmlSanitizer _sanitizer = new HtmlSanitizer();
            this.Userlogin = _sanitizer.Sanitize(this.Userlogin);
            this.Pass = _sanitizer.Sanitize(this.Pass);
        }
    }
}
