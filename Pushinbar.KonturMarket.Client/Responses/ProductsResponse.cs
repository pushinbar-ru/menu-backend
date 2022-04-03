using System.Collections.Generic;
using Pushinbar.KonturMarket.Client.Models;

namespace Pushinbar.KonturMarket.Client.Responses
{
    public class ProductsResponse
    {
        public IEnumerable<Product> Items { get; set; }
    }
}
