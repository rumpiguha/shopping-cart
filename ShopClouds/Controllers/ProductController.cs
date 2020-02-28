using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            _productService = productService;
        }

        [HttpGet, Route("getAll")]
        public async Task<ActionResult<List<Product>>> GetProductList()
        {
           return await _productService.GetProductListAsync();
        }

    }
}