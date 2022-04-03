using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pushinbar.Common.Enums;

namespace Pushinbar.Common.Models
{
    public interface IProduct
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
    }
}
