using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pushinbar.Common.DTOs;
using Pushinbar.Common.DTOs.Snack;
using Pushinbar.Common.Exstensions;
using Pushinbar.Services.Products;

namespace Pushinbar.API.Controllers
{
    [ApiController]
    [Route("products/[controller]")]
    public class SnacksController : ControllerBase
    {
        private readonly IProductsService productsService;
        private readonly ILogger<SnacksController> logger;
        
        public SnacksController(IProductsService productsService, ILogger<SnacksController> logger)
        {
            this.productsService = productsService;
            this.logger = logger;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SnackProductDto>>> GetAsync()
        {
            var result = Enumerable.Empty<SnackProductDto>();
            
            try
            {
                var products = await productsService.GetSnackProductsAsync();
                result = products?.Select(x => x.ToDto());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when getting snack products");
            }

            return Ok(result);
        }
        
        [HttpPut]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateAlcoholProductsAsync([FromQuery] Guid id, [FromBody] SnackUpdateProductDto snackUpdateProductDto)
        {
            return Ok();
        }
    }
}