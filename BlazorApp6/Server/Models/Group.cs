using System;
using System.Collections.Generic;

namespace BlazorApp6.Server.Models
{
    public partial class Group
    {
        public Group()
        {
            ItemsGroups = new HashSet<ItemsGroup>();
        }

        public int Idgroup { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; } = null!;

        public virtual User? User { get; set; }
        public virtual ICollection<ItemsGroup> ItemsGroups { get; set; }
    }
}
