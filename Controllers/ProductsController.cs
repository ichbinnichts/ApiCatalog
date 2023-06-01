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
                return NotFound();
            }
            return Ok(products);
        }
    }
}
