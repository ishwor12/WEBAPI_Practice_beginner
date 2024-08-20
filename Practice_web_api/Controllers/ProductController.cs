using Domain;
using Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Practice_web_api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static readonly List<Product> _product = new();
        private readonly ApplicationContext _context;

        public ProductController(ApplicationContext context)
        {
            _context = context;
        }



       

        [HttpPost]
        public async Task<IActionResult> postdata(Product product)
        {
            _context.products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = product.Id }, product);

        }





        [HttpGet]
        public async Task<IActionResult> GetALL([FromQuery] PaginationParameters paginationParameters)
        {
            var products = _context.products.AsQueryable();
            
            try
            {
                var totalItems = await products.CountAsync();
                var items = await products.Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                                           .Take(paginationParameters.PageSize)
                                           .ToListAsync();
                if (items == null)
                {
                    throw new NoContentException($"Product  are null.");

                }
                var paginationMetadata = new
                                        {
                                            totalCount = totalItems,
                                            pageSize = paginationParameters.PageSize,
                                            currentPage = paginationParameters.PageNumber,
                                            totalPages = (int)Math.Ceiling(totalItems / (double)paginationParameters.PageSize),
                                            Items = items
                };

               
                return Ok(paginationMetadata);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse { Message = ex.Message });

            }
            catch (Exception ex) { return StatusCode(500, "An unexpected error occurred."); }



        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyID(int id)
        {
            var product = _context.products.FindAsync(id);
            if (product == null)
            {
                throw new NoContentException($"Product with {id} not found.");
            }
            return Ok(product);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            var existingproduct = await _context.products.FindAsync(id);

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
            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                {
                    return NotFound();


                }
            }
            _context.products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        }
}
