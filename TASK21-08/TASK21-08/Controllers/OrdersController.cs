using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TASK21_08.Models;

namespace TASK21_08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDbContext _db;

        public OrdersController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {

            var category = _db.Orders.ToList();
            return Ok(category);
        }

        [HttpGet("{id:int:min(5)}")]
        public IActionResult getById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var oredId = _db.Orders.FirstOrDefault(l => l.OrderId == id);

            return Ok(oredId);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(int name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            var userOrders = _db.Orders.FirstOrDefault(l => l.User.UserId == name);
            if (userOrders == null)
            {

                return BadRequest();
            }

            return Ok(userOrders);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var deleteorder = _db.Orders.FirstOrDefault(c => c.OrderId == id);

            _db.Orders.Remove(deleteorder);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
