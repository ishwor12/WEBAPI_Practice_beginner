using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Practice_web_api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static readonly List<Product> _product = new();
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }



        static ProductController()
        {
            _product.Add(new Product { Id = 1, Name = "Electronics", Price = 2000 });

        }

        [HttpPost]
        public async Task<IActionResult> postdata(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = product.Id }, product);

        }





        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var product = await _context.Products.ToListAsync();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyID(int id)
        {
            var product = _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            var existingproduct = await _context.Products.FindAsync(id);

            //var existingproduct = _product.FirstOrDefault(y => y.Id == id);
            if (existingproduct == null)
            {
                return NotFound();
            }

            existingproduct.Name = product.Name;
            existingproduct.Price = product.Price;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(existingproduct);
            }
            catch(Exception ex) 
            {
                throw;
            }
         }
        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                {
                    return NotFound();


                }
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        }
}
