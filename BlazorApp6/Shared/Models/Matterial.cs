using System;
using System.Collections.Generic;

namespace BlazorApp6.Shared.Models
{
    public partial class Matterial
    {
        public Matterial()
        {
            ItemsMatterials = new HashSet<ItemsMatterial>();
        }

        public int Idmat { get; set; }
        public string Materialname { get; set; } = null!;
        public string? Descriptions { get; set; }
        public int? TempOfWash { get; set; }

        public virtual ICollection<ItemsMatterial> ItemsMatterials { get; set; }
    }
}
