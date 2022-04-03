using System.Collections.Generic;
using Pushinbar.KonturMarket.Client.Models;

namespace Pushinbar.KonturMarket.Client.Responses
{
    public class ProductGroupsResponse
    {
        public IEnumerable<ProductGroup> Items { get; set; }
    }
}
