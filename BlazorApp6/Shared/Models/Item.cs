using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorApp6.Shared.Models
{
    public partial class Item
    {
        public Item()
        {
            ItemsGroups = new HashSet<ItemsGroup>();
            MaterialsItems = new HashSet<MaterialsItem>();
        }

        public int Iditem { get; set; }
        [StringLength(128, ErrorMessage = "The name must be at most 128 characters long.")]
        public string Name { get; set; } = null!;
        [StringLength(512, ErrorMessage = "The description must be at most 512 characters long.")]
        public string? Descriptions { get; set; }
        [Range(0.0, float.MaxValue, ErrorMessage = "The weight must be a positive float.")]
        public float? WeightIt { get; set; }

        [Range(0.0, float.MaxValue, ErrorMessage = "The height must be a positive float.")]
        public float? HeightIt { get; set; }

        [Range(0.0, float.MaxValue, ErrorMessage = "The width must be a positive float.")]
        public float? WidthIt { get; set; }

        [Range(0.0, float.MaxValue, ErrorMessage = "The length must be a positive float.")]
        public float? LengthIt { get; set; }
        public DateTime? DateOfPurchase { get; set; }

        [Range(0, Int16.MaxValue, ErrorMessage = "Number of washes must be a positive float.")]
        public Int16? NrOfWashes { get; set; }
        public DateTime? LastWash { get; set; }
        public bool DangerousColor { get; set; }

        [JsonIgnore]
        public virtual ICollection<ItemsGroup> ItemsGroups { get; set; }

        [JsonIgnore]
        public virtual ICollection<MaterialsItem> MaterialsItems { get; set; }
    }
}