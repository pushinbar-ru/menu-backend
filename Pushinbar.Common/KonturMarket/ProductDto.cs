using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pushinbar.Common.KonturMarket
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string ShopId { get; set; }
        public string ProductType { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string GroupId { get; set; }
        public string[] Barcodes { get; set; }
        public string TaxSystem { get; set; }
        public string VatRate { get; set; }
        public float SellPricePerUnit { get; set; }
    }
}
