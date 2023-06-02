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

            try
            {
                var products = _context.Products.AsNoTracking().Take(10).ToList();
                if (products == null)
                {
                    return NotFound("Products not found...");
                }
                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while handling your request");
            }
            
        }
        [HttpGet("{id:int}", Name ="GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                var product = _context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == id);
                if (product == null)
                {
                    return NotFound("Product not found...");
                }
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while handling your request");
            }
            
        }
        [HttpPost]
        public ActionResult Post(Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                _context.Products.Add(product);
                _context.SaveChanges();

                return new CreatedAtRouteResult("GetProduct",
                    new { id = product.ProductId }, product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while handling your request");
            }
            
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    return BadRequest();
                }
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while handling your request");
            }
            

            return Ok(product);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
                if (product == null)
                {
                    return NotFound("Product not found...");
                }
                _context.Products.Remove(product);
                _context.SaveChanges();

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while handling your request");
            }
            
        }
    }
}
