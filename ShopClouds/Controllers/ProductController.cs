using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Interfaces;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet, Route("getAll")]
        public async Task<IActionResult> GetProductList()
        {
           return new OkObjectResult(await _productService.GetProductListAsync());
        }

    }
}