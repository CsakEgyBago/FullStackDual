using Dual.Server.Dtos;
using Eszi.Demo.Database;
using Eszi.Demo.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Dual.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private CoreDbContext coreDbContext;

        public ProductController(CoreDbContext coreDbContext)
        {
            this.coreDbContext = coreDbContext;
        }

        Func<Product, ProductDto> mapProductToDto = p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Description = p.Description
        };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await coreDbContext.Products.ToListAsync();
            var mapped = products.Select(mapProductToDto);
            return Ok(mapped);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ProductDto>> GetById(long id)
        {
            var product = await coreDbContext.Products.SingleOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            var mapped = mapProductToDto(product);
            return Ok(mapped);
        }

        Func<ProductDto, Product> mapProductDtoToProduct = p => new Product
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Description = p.Description
        };

        [HttpPost]
        [Authorize(Roles = BuiltInRoles.Admin)]
        public async Task<ActionResult<Product>> Post(ProductDto product)
        {
            var mapped = mapProductDtoToProduct(product);

            await coreDbContext.Products.AddAsync(mapped);
            await coreDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = mapped.Id }, mapProductToDto(mapped));
        }
        [HttpDelete("{id:long}")]
        [Authorize(Roles = BuiltInRoles.Admin)]
        public async Task<ActionResult> Delete(long id)
        {
            var product = await coreDbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            coreDbContext.Products.Remove(product);
            await coreDbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = BuiltInRoles.Admin)]
        public async Task<ActionResult<ProductDto>> Put( ProductDto productDto)
        {

            var existingProduct = await coreDbContext.Products.SingleOrDefaultAsync(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            await coreDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
