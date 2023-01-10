using System.Text.Json.Serialization;

namespace BlazorApp6.Shared.Models
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

        [JsonIgnore]
        public virtual ICollection<ItemsGroup> ItemsGroups { get; set; }
    }
}