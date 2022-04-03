using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pushinbar.KonturMarket.Client.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid ShopId { get; set; }
        public string ProductType { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public Guid GroupId { get; set; }
        public string[] Barcodes { get; set; }
        public string TaxSystem { get; set; }
        public string VatRate { get; set; }
        public float SellPricePerUnit { get; set; }
    }
}
