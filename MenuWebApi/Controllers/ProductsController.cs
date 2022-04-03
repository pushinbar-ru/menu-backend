using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pushinbar.Common.DTOs;
using Pushinbar.Common.Exstensions;
using Pushinbar.Services.Products;

namespace Pushinbar.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;
        private readonly ILogger<ProductsController> logger;
        
        public ProductsController(IProductsService productsService, ILogger<ProductsController> logger)
        {
            this.productsService = productsService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("alcohol")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AlcoholProductDto>>> GetAlcoholProductsAsync()
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
        
        [HttpGet]
        [Route("notalcohol")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<NotAlcoholProductDto>>> GetNotAlcoholProductsAsync()
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
        
        [HttpGet]
        [Route("eat")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EatProductDto>>> GetEatProductsAsync()
        {
            var result = Enumerable.Empty<EatProductDto>();
            
            try
            {
                var products = await productsService.GetEatProductsAsync();
                result = products?.Select(x => x.ToDto());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when getting eat products");
            }

            return Ok(result);
        }
        
        [HttpGet]
        [Route("snacks")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SnackProductDto>>> GetSnackProductsAsync()
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
    }
}