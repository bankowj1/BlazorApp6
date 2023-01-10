using System;
using System.Collections.Generic;

namespace BlazorApp6.Shared.Models
{
    public partial class User
    {
        public User()
        {
            Groups = new HashSet<Group>();
        }

        public int Iduser { get; set; }
        public string Username { get; set; } = null!;
        public byte[] Userlogin { get; set; } = null!;
        public byte[] Pass { get; set; } = null!;
        public byte[] Email { get; set; } = null!;

        public virtual ICollection<Group> Groups { get; set; }
    }
}
