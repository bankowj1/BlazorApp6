using System;
using System.Collections.Generic;

namespace BlazorApp6.Server.Models
{
    public partial class Item
    {
        public Item()
        {
            ItemsGroups = new HashSet<ItemsGroup>();
            MaterialsItems = new HashSet<MaterialsItem>();
        }

        public int Iditem { get; set; }
        public string Name { get; set; } = null!;
        public string? Descriptions { get; set; }
        public float? WeightIt { get; set; }
        public float? HeightIt { get; set; }
        public float? WidthIt { get; set; }
        public float? LengthIt { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public int? NrOfWashes { get; set; }
        public DateTime? LastWash { get; set; }
        public bool DangerousColor { get; set; }

        public virtual ICollection<ItemsGroup> ItemsGroups { get; set; }
        public virtual ICollection<MaterialsItem> MaterialsItems { get; set; }
    }
}
