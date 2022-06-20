using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pushinbar.Common.DTOs.Alcohol;
using Pushinbar.Common.DTOs.Untappd;
using Pushinbar.Untappd.Client;

namespace Pushinbar.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UntappdController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<UntappdBeerInfoDto>> GetAsync([FromQuery] string sourceUrl )
        {
            var beerInfo = await UntappdClient.GetBeerInformationByUrlAsync(sourceUrl);
            var result = new UntappdBeerInfoDto()
            { 
                UntappdUrl = beerInfo.UntappdUrl,
                Description = beerInfo.Description,
                Alc = beerInfo.Alc,
                Brewery = beerInfo.Brewery,
                IBU = beerInfo.IBU,
                Subcategory = beerInfo.Subcategory
            };
            return Ok(result);
        }
    }
}