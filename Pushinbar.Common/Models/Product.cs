using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pushinbar.Common.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public float Price { get; set; }
        public string Categories { get; set; }
        public float Rest { get; set; }
        public string Status { get; set; }
        public int LikesCount { get; set; }
    }
}
