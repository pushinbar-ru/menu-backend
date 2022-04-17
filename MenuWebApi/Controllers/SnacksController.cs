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
using Pushinbar.Common.Models.Snack;
using Pushinbar.Services.Products;

namespace Pushinbar.API.Controllers
{
    [ApiController]
    [Route("products/[controller]")]
    public class SnacksController : ControllerBase
    {
        private readonly IProductsService<SnackProduct> productsService;
        private readonly ILogger<SnacksController> logger;
        
        public SnacksController(IProductsService<SnackProduct> productsService, ILogger<SnacksController> logger)
        {
            this.productsService = productsService;
            this.logger = logger;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SnackProductDto>>> GetAllAsync()
        {
            var result = Enumerable.Empty<SnackProductDto>();
            
            try
            {
                var products = await productsService.GetAllAsync();
                result = products?.Select(x => x.ToDto());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when getting snack products");
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<SnackProductDto>> GetAsync([FromQuery] Guid id)
        {
            return Ok(new SnackProductDto());
        }
        
        [HttpPut]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] SnackUpdateProductDto snackUpdateProductDto)
        {
            return Ok();
        }
    }
}