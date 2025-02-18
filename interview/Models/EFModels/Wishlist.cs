namespace interview.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Wishlist")]
    public partial class Wishlist
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int? ProductId { get; set; }

        public virtual Products Products { get; set; }

        public virtual Users Users { get; set; }
    }
}
