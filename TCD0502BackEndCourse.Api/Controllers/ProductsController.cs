using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TCD0502BackEndCourse.Api.Models;
using TCD0502BackEndCourse.Api.Repositories.Interface;

namespace TCD0502BackEndCourse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        // Endpoint: /api/products
        [HttpGet("")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }
        // Endpoint: /api/products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productRepository.GetProduct(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost("create")]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = _productRepository.Create(product);

            return result ? StatusCode(StatusCodes.Status201Created)
                : BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productRepository.Delete(id);
            return result ? StatusCode(StatusCodes.Status200OK) : BadRequest();

        }

        [HttpPut("edit/{id}")]
        public IActionResult Edit(int id, Product product)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = _productRepository.Edit(id, product);

            return result ? StatusCode(StatusCodes.Status204NoContent) : BadRequest();
        }
    }
}
