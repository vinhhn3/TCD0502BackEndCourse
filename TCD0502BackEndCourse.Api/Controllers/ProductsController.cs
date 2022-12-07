using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Linq;

using TCD0502BackEndCourse.Api.Data;
using TCD0502BackEndCourse.Api.Models;

namespace TCD0502BackEndCourse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Endpoint: /api/products
        [HttpGet("")]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }
        // Endpoint: /api/products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost("create")]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid) return BadRequest();
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            _context.Add(newProduct);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            _context.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public IActionResult Edit(int id, Product product)
        {
            if (!ModelState.IsValid) return BadRequest();
            var productInDb = _context.Products
                .SingleOrDefault(p => p.Id == id);

            if (productInDb == null) return NotFound();

            productInDb.Name = product.Name;
            productInDb.Description = product.Description;
            productInDb.Price = product.Price;

            _context.SaveChanges();

            return NoContent();
        }
    }
}
