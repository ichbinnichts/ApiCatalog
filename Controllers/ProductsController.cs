using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            //TIPS:
            //Using AsNoTracking() in properties that doesnt alter the values in the db
            // you will get a better app performance

            //Never get all records in a get method, it will overcharge the app. Use Take(10) as example
            var products = _context.Products.AsNoTracking().Take(10).ToList();
            if(products == null)
            {
                return NotFound("Products not found...");
            }
            return Ok(products);
        }
        [HttpGet("{id:int}", Name ="GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = _context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == id);
            if(product == null)
            {
                return NotFound("Product not found...");
            }
            return Ok(product);
        }
        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product == null)
                return BadRequest();
            
            _context.Products.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProduct",
                new { id = product.ProductId }, product);
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if(id != product.ProductId)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(product);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if(product == null)
            {
                return NotFound("Product not found...");
            }
            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}
