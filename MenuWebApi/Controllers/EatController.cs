using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pushinbar.Common.DTOs;
using Pushinbar.Common.DTOs.Eat;
using Pushinbar.Common.Exstensions;
using Pushinbar.Common.Models.Eat;
using Pushinbar.Services.Products;

namespace Pushinbar.API.Controllers
{
    [ApiController]
    [Route("products/[controller]")]
    public class EatController : ControllerBase
    {
        private readonly IProductsService<EatProduct> productsService;
        private readonly ILogger<EatController> logger;
        
        public EatController(IProductsService<EatProduct> productsService, ILogger<EatController> logger)
        {
            this.productsService = productsService;
            this.logger = logger;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EatProductDto>>> GetAllAsync()
        {
            var result = Enumerable.Empty<EatProductDto>();
            
            try
            {
                var products = await productsService.GetAllAsync();
                result = products?.Select(x => x.ToDto());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when getting eat products");
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<EatProductDto>> GetAsync([FromQuery] Guid id)
        {
            return Ok(new EatProductDto());
        }
        
        [HttpPut]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] EatUpdateProductDto eatUpdateProductDto)
        {
            return Ok();
        }
    }
}