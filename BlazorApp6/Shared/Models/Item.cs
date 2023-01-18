using System;
using System.Collections.Generic;

namespace BlazorApp6.Shared.Models
{
    public partial class Item
    {
        public Item()
        {
            GroupsItems = new HashSet<GroupsItem>();
            ItemsMatterials = new HashSet<ItemsMatterial>();
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

        public virtual ICollection<GroupsItem> GroupsItems { get; set; }
        public virtual ICollection<ItemsMatterial> ItemsMatterials { get; set; }
    }
}
