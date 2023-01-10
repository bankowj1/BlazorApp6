using System;
using System.Collections.Generic;

namespace BlazorApp6.Server.Models
{
    public partial class MaterialsItem
    {
        public int IdmaterialsItem { get; set; }
        public int ItemId { get; set; }
        public int MatId { get; set; }
        public int? Layer { get; set; }
        public int? Content { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual Matterial Mat { get; set; } = null!;
    }
}
