using System.Collections.Generic;
using Pushinbar.Common.DTOs.Interfaces;
using Pushinbar.Common.Enums;

namespace Pushinbar.Common.DTOs.Eat
{
    public class EatUpdateProductDto : IUpdateProductDto
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public ProductStatus Status { get; set; }
        public string Barcode { get; set; }
        public IEnumerable<string> Subcategories { get; set; }
    }
}