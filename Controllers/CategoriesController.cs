using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _context.Categories.ToList();
            if(categories == null)
            {
                return NotFound("Categories not found...");
            }
            return Ok(categories);
        }
        [HttpGet("{id:int}", Name ="GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);
            if(category == null)
            {
                return NotFound("Category not found...");
            }
            return Ok(category);
        }
        [HttpPost]
        public ActionResult Post(Category category)
        {
            if(category == null)
            {
                return BadRequest();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return new CreatedAtRouteResult("GetCategory",
                new { id = category.CategoryId }, category);
        }
    }
}
