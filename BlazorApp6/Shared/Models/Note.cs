using System;
using System.Collections.Generic;

namespace BlazorApp6.Shared.Models
{
    public partial class Note
    {
        public int Idnotes { get; set; }
        public byte[] Note1 { get; set; } = null!;
    }
}
