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
using Pushinbar.Common.Models.NotAlcohol;
using Pushinbar.Services.Products;

namespace Pushinbar.API.Controllers
{
    [ApiController]
    [Route("products/[controller]")]
    public class NotAlcoholController : ControllerBase
    {
        private readonly IProductsService<NotAlcoholProduct> productsService;
        private readonly ILogger<NotAlcoholController> logger;
        
        public NotAlcoholController(IProductsService<NotAlcoholProduct> productsService, ILogger<NotAlcoholController> logger)
        {
            this.productsService = productsService;
            this.logger = logger;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<NotAlcoholProductDto>>> GetAllAsync()
        {
            var result = Enumerable.Empty<NotAlcoholProductDto>();
            
            try
            {
                var products = await productsService.GetAllAsync();
                result = products?.Select(x => x.ToDto());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when getting not alcohol products");
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<NotAlcoholProductDto>> GetAsync([FromQuery] Guid id)
        {
            var product = await productsService.GetAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        
        [HttpPut]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] NotAlcoholUpdateProductDto notAlcoholUpdateProductDto)
        {
            var alcoholUpdateProduct = new NotAlcoholUpdateProduct();
            alcoholUpdateProduct.UpdateFromDto(notAlcoholUpdateProductDto);
            var result = await productsService.TryUpdateAsync(id, alcoholUpdateProduct);
            if (!result)
                return NotFound();
            return Ok();
        }
    }
}