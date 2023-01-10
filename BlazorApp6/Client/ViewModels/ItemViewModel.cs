using System.ComponentModel.DataAnnotations;

namespace BlazorApp6.ViewModels
{
    public class ItemViewModel
    {
        [StringLength(128, ErrorMessage = "The name must be at most 128 characters long.")]
        public string Name { get; set; } = null!;

        [StringLength(512, ErrorMessage = "The description must be at most 512 characters long.")]
        public string? Descriptions { get; set; }

        [Range(0, Int16.MaxValue, ErrorMessage = "Number of washes must be a positive float.")]
        public Int16? NrOfWashes { get; set; }

        public DateTime? LastWash { get; set; }
        public bool DangerousColor { get; set; }
    }
}