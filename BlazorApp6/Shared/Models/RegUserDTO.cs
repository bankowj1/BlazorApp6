﻿using System;
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
    }
}
