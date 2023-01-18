using System;
using System.Collections.Generic;

namespace BlazorApp6.Shared.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupsItems = new HashSet<GroupsItem>();
        }

        public int Idgroup { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; } = null!;

        public virtual User? User { get; set; }
        public virtual ICollection<GroupsItem> GroupsItems { get; set; }
    }
}
