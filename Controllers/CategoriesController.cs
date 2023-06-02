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
            if(categories is null)
            {
                return NotFound("Categories not found...");
            }
            return Ok(categories);
        }
        [HttpGet("{id:int}")]
        public ActionResult<Category> Get(int id)
        {
            var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);
            if(category is null)
            {
                return NotFound("Category not found...");
            }
            return Ok(category);
        }
    }
}
