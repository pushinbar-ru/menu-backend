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
using Pushinbar.Common.Models.Alcohol;
using Pushinbar.Services.Products;

namespace Pushinbar.API.Controllers
{
    [ApiController]
    [Route("products/[controller]")]
    public class AlcoholController : ControllerBase
    {
        private readonly IProductsService<AlcoholProduct> productsService;
        private readonly ILogger<AlcoholController> logger;
        
        public AlcoholController(IProductsService<AlcoholProduct> productsService, ILogger<AlcoholController> logger)
        {
            this.productsService = productsService;
            this.logger = logger;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AlcoholProductDto>>> GetAllAsync()
        {
            var result = Enumerable.Empty<AlcoholProductDto>();
            
            try
            {
                var products = await productsService.GetAllAsync();
                result = products?.Select(x => x.ToDto());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when getting alcohol products");
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AlcoholProductDto>> GetAsync([FromQuery] Guid id)
        {
            var product = await productsService.GetAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        
        [HttpPut]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] AlcoholUpdateProductDto alcoholUpdateProductDto)
        {
            return Ok();
        }
    }
}