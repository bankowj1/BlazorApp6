using System;
using System.Collections.Generic;

namespace BlazorApp6.Shared.Models
{
    public partial class Matterial
    {
        public Matterial()
        {
            MaterialsItems = new HashSet<MaterialsItem>();
        }

        public int Idmat { get; set; }
        public string Materialname { get; set; } = null!;
        public string? Descriptions { get; set; }
        public int? TempOfWash { get; set; }

        public virtual ICollection<MaterialsItem> MaterialsItems { get; set; }
    }
}
