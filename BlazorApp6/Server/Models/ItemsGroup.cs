using System;
using System.Collections.Generic;

namespace BlazorApp6.Server.Models
{
    public partial class ItemsGroup
    {
        public int IditemsGroup { get; set; }
        public int? GroupId { get; set; }
        public int? ItemId { get; set; }
        public int? NumberIt { get; set; }

        public virtual Group? Group { get; set; }
        public virtual Item? Item { get; set; }
    }
}
