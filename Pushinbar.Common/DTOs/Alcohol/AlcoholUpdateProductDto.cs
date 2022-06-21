using System.Collections.Generic;
using Pushinbar.Common.DTOs.Interfaces;
using Pushinbar.Common.Enums;

namespace Pushinbar.Common.DTOs.Alcohol
{
    public class AlcoholUpdateProductDto : IUpdateProductDto
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public ProductStatus Status { get; set; }
        public string Barcode { get; set; }
        public IEnumerable<string> Subcategories { get; set; }
        public string IBU { get; set; }
        public string Alc { get; set; }
        public string UntappdUrl { get; set; }
        public string Brewery { get; set; }
        public string  Volume { get; set; }
    }
}