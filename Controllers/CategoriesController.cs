using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly APICatalogContext _context;
        public CategoriesController(APICatalogContext context)
        {
            _context = context;
        }
        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoryProducts()
        {
            //TIPS:
            //Never return related objects without filters, it will overcharge the app
            //Use Where(c => CategoryId <= 5) as example

            try
            {
                return _context.Categories.Include(p => p.Products).Where(c => c.CategoryId <= 5).ToList();
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "An error ocurred while handling your request");
            }
            
        }


        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            //TIPS:
            //Using AsNoTracking() in properties that doesnt alter the values in the db
            // you will get a better app performance

            //Never get all records in a get method, it will overcharge the app. Use Take(10) as example

            try
            {
                var categories = _context.Categories.AsNoTracking().Take(10).ToList();
                if (categories == null)
                {
                    return NotFound("Categories not found...");
                }
                return Ok(categories);
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while handling your request");
            }
            
        }
        [HttpGet("{id:int}", Name ="GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            try
            {
                var category = _context.Categories.AsNoTracking().FirstOrDefault(c => c.CategoryId == id);
                if (category == null)
                {
                    return NotFound("Category not found...");
                }
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while handling your request");
            }
            
        }
        [HttpPost]
        public ActionResult Post(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }
                _context.Categories.Add(category);
                _context.SaveChanges();
                return new CreatedAtRouteResult("GetCategory",
                    new { id = category.CategoryId }, category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while handling your request");
            }
            
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return BadRequest();
                }
                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while handling your request");
            }
            
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
                if (category == null)
                {
                    return NotFound("Category not found...");
                }
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "An error occurred while handling your request");
            }
            
        }
    }
}
