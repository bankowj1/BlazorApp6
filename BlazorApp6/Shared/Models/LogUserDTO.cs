using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp6.Shared.Models
{
    public partial class LogUserDTO
    {
        public byte[] Userlogin { get; set; } = null!;
        public byte[] Pass { get; set; } = null!;
    }
}
