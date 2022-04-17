using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pushinbar.Common.DTOs;
using Pushinbar.Common.DTOs.NotAlcohol;
using Pushinbar.Common.Exstensions;
using Pushinbar.Services.Products;

namespace Pushinbar.API.Controllers
{
    [ApiController]
    [Route("products/[controller]")]
    public class NotAlcoholController : ControllerBase
    {
        private readonly IProductsService productsService;
        private readonly ILogger<NotAlcoholController> logger;
        
        public NotAlcoholController(IProductsService productsService, ILogger<NotAlcoholController> logger)
        {
            this.productsService = productsService;
            this.logger = logger;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<NotAlcoholProductDto>>> GetAsync()
        {
            var result = Enumerable.Empty<NotAlcoholProductDto>();
            
            try
            {
                var products = await productsService.GetNotAlcoholProductsAsync();
                result = products?.Select(x => x.ToDto());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when getting not alcohol products");
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