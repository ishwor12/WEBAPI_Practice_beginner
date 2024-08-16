using Microsoft.AspNetCore.Mvc;

namespace Practice_web_api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static readonly List<Product> _product = new();
        static ProductController()
        {
            _product.Add(new Product { Id = 1, Name = "Electronics", Price = 2000 });

        }

        [HttpPost]
        public IActionResult postdata([FromBody] Product product)
        {
            _product.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);

        }





        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _product;
        }

        [HttpGet("{id}")]
        public IActionResult GetbyID(int id)
        {
            var product = _product.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }
        [HttpPut("{id}")]
        public IActionResult Put (int id, [FromBody] Product product)
        {
            var existingproduct = _product.FirstOrDefault(y => y.Id == id);
            if (existingproduct == null)
            {
                return NotFound();
            }
            existingproduct.Name = product.Name;
            existingproduct.Price = product.Price;
            return Ok(existingproduct);
        }
    }
}
