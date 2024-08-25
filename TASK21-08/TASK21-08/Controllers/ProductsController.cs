using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TASK21_08.Models;

namespace TASK21_08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;
        public ProductsController(MyDbContext db) { _db = db; }

        [HttpGet]
        public IActionResult Get()
        {
            var product = _db.Products.ToList();
            return Ok(product);
        }

        [HttpGet("{id:int:max(10)}")]
        public IActionResult GetById(int id) { 
            if (id <= 0)
            {

            return BadRequest(); 
            }
            
            var product = _db.Products.Where(l => l.ProductId == id).FirstOrDefault();

            return Ok(product);
        }

        [HttpGet("/ProductById/{id}")]
        public IActionResult GetByProductId(int id)
        {
            if (id == null)
            {
                var products = _db.Products.ToList();
                return BadRequest(products);
            }

            var product = _db.Products.Where(l => l.ProductId == id).ToList();

            return Ok(product);
        }

        [HttpGet("/ProductByCategoryId/{id}")]
        public IActionResult GetByCategoryId(int id)
        {
            if (id == null)
            {
                var products = _db.Products.ToList();
                return BadRequest(products);
            }

            var product = _db.Products.Where(l => l.CategoryId == id).ToList();

            return Ok(product);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name) {
            if (name == null) { 
                return BadRequest();
            }
            var productName = _db.Products.Where(l => l.ProductName == name);
            if (productName == null) {
                return NotFound();


            }
            return Ok(productName);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var product = _db.Products.Where(l => l.ProductId == id).FirstOrDefault();

            var deleteOrders = _db.Orders.Where(l => l.UserId == id).ToList();
            foreach (var order in deleteOrders)
            {

                _db.Orders.Remove(order);
            }

            _db.Products.Remove(product);
            _db.SaveChanges();
            return Ok();
        }



    }
}
