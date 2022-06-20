using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pushinbar.Common.DTOs.Untappd
{
    public class UntappdBeerInfoDto
    {
        public string UntappdUrl { get; set; }
        public string Description { get; set; }
        public string Alc { get; set; }
        public string Subcategory { get; set; }
        public string IBU { get; set; }
        public string Brewery { get; set; }
    }
}
