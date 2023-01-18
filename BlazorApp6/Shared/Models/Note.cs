using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApp6.Shared.Models
{
    public partial class Note
    {
        public Note()
        {
            Users = new HashSet<User>();
        }

        public int Idnotes { get; set; }
        public byte[] Note1 { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
