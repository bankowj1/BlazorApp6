using System;
using System.Collections.Generic;

namespace BlazorApp6.Shared.Models
{
    public partial class NotesUser
    {
        public int? NotesId { get; set; }
        public int? UserId { get; set; }

        public virtual Note? Notes { get; set; }
        public virtual User? User { get; set; }
    }
}
