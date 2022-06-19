using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pushinbar.Common.DTOs.Alcohol;
using Pushinbar.Untappd.Client;

namespace Pushinbar.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UntappdController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<AlcoholProductDto>> GetAsync([FromQuery] string sourceUrl )
        {
            var result = await UntappdClient.GetBeerInformationByUrlAsync(sourceUrl);
            return Ok(result);
        }
    }
}