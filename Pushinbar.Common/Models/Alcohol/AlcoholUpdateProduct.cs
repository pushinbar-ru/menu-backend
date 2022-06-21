using System;
using System.Collections.Generic;
using Pushinbar.Common.Enums;
using Pushinbar.Common.Models.Interfaces;

namespace Pushinbar.Common.Models.Alcohol
{
    public class AlcoholUpdateProduct : IUpdateProduct
    {
        public Guid Id { get; set; }
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