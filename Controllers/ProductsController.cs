using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly APICatalogContext _context;
        public ProductsController(APICatalogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = this._context.Products.ToList();
            if(products == null)
            {
                return NotFound("Products not found");
            }
            return Ok(products);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = this._context.Products.FirstOrDefault(p => p.ProductId == id);
            if(product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }
    }
}
