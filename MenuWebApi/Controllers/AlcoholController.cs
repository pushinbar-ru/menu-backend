using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pushinbar.Common.DTOs;
using Pushinbar.Common.DTOs.Alcohol;
using Pushinbar.Common.Exstensions;
using Pushinbar.Services.Products;

namespace Pushinbar.API.Controllers
{
    [ApiController]
    [Route("products/[controller]")]
    public class AlcoholController : ControllerBase
    {
        private readonly IProductsService productsService;
        private readonly ILogger<AlcoholController> logger;
        
        public AlcoholController(IProductsService productsService, ILogger<AlcoholController> logger)
        {
            this.productsService = productsService;
            this.logger = logger;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AlcoholProductDto>>> GetAsync()
        {
            var result = Enumerable.Empty<AlcoholProductDto>();
            
            try
            {
                var products = await productsService.GetAlcoholProductsAsync();
                result = products?.Select(x => x.ToDto());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when getting alcohol products");
            }

            return Ok(result);
        }
        
        [HttpPut]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateAlcoholProductsAsync([FromQuery] Guid id)
        {
            return Ok();
        }
    }
}