using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Pushinbar.Common.Enums;

namespace Pushinbar.Common.Entities
{
    public class EatEntity : IProductEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid KonturMarketId { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public ProductType Type { get; set; }
        public ProductStatus Status { get; set; }
        public int LikesCount { get; set; }
        public string Barcode { get; set; }
        public string Subcategories { get; set; }
    }
}