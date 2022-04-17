using System;
using System.Collections.Generic;
using Pushinbar.Common.Enums;
using Pushinbar.Common.Models.Interfaces;

namespace Pushinbar.Common.Models.Eat
{
    public class EatUpdateProduct : IUpdateProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public ProductStatus Status { get; set; }
        public string Barcode { get; set; }
        public IEnumerable<string> Subcategories { get; set; }
    }
}