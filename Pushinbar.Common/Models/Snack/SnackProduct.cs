using System;
using System.Collections.Generic;
using Pushinbar.Common.Enums;
using Pushinbar.Common.Models.Interfaces;

namespace Pushinbar.Common.Models.Snack
{
    public class SnackProduct : IProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public ProductType Type { get; set; }
        public float Rest { get; set; }
        public ProductStatus Status { get; set; }
        public int LikesCount { get; set; }
        public string Barcode { get; set; }
        public IEnumerable<string> Subcategories { get; set; }
    }
}