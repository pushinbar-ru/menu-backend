using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pushinbar.Common.KonturMarket;

namespace Pushinbar.KonturMarket.Client.Responses
{
    public class ProductsResponse
    {
        public IEnumerable<ProductDto> Items { get; set; }
    }
}
