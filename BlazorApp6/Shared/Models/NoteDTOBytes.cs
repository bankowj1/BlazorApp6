using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp6.Shared.Models
{
    public class NoteDTOBytes
    {
        public byte[] Note1 { get; set; } = null!;
        public bool IsCoded { get; set; }
    }
}
