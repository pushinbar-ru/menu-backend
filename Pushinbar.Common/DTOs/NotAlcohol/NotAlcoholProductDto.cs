using System;
using System.Collections.Generic;
using Pushinbar.Common.DTOs.Interfaces;
using Pushinbar.Common.Enums;

namespace Pushinbar.Common.DTOs.NotAlcohol
{
    public class NotAlcoholProductDto : IProductDto
    {
        public string Alc { get; set; }
        public string IBU { get; set; }
        public IEnumerable<string> Subcategories { get; set; }
        public string UntappdUrl { get; set; }
        public string Brewery { get; set; }
        public string  Volume { get; set; }
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
    }
}