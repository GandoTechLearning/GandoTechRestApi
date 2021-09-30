using Domain.DataService;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryApi.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        //GET: .../api/product
        [HttpGet]
        [Authorize("read")]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await _productService.GetProductsAsync();

            if (products == null || products.Count == 0)
                return NotFound();

            return Ok(products);
        }

        //GET: .../api/product/3
        [Authorize("read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _productService.GetProductAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        //POST: .../api/product
        [HttpPost]
        [Authorize("write")]
        public async Task<ActionResult> Post(Product product)
        {
            var result = await _productService.AddProductAsync(product);
            if (!result)
                return BadRequest("Value is null or duplicated.");
            return Ok(true);
        }

        //POST: .../api/product/3
        [HttpPost("{id}")]
        [Authorize("write")]
        public async Task<ActionResult<bool>> Post(int id, Product product)
        {
            var result = await _productService.InsertProductAsync(id, product);
            if (!result)
                return BadRequest("Value is null or duplicated.");
            return Ok(true);
        }

        //PUT: .../api/product/3
        [HttpPut("{id}")]
        [Authorize("write")]
        public async Task<ActionResult<bool>> Put(int id, Product productTo)
        {
            var result = await _productService.UpdateProductAsync(id, productTo);
            if (!result)
                return BadRequest("Product not found.");
            return Ok(true);
        }

        //DELETE: .../api/product/3
        [HttpDelete("{id}")]
        [Authorize("write")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
                return BadRequest("Product not found.");
            return Ok(true);
        }
    }
}
