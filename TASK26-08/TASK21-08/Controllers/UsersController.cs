using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TASK21_08.DTOs;
using TASK21_08.Models;

namespace TASK21_08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _db;

        public UsersController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {

            var category = _db.Users.ToList();
            return Ok(category);
        }

        [HttpPost]
        public IActionResult postProduct([FromForm] usersDTOs userDTO)
        {
            

            var user = new User
            {
                Username = userDTO.Username,
                Password = userDTO.Password,
                Email = userDTO.Email,
                
            };

            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult putProduct(int id, [FromForm] usersDTOs userDTO)
        {
            
            var x = _db.Users.FirstOrDefault(l => l.UserId == id);


            x.Username = userDTO.Username;
            x.Password = userDTO.Password;
            x.Email = userDTO.Email;


            _db.Users.Update(x);

            _db.SaveChanges();
            return Ok(x);
        }

        [Route("{id:int:min(5)}")]
        [HttpGet]
        public IActionResult getById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var categoryId = _db.Users.FirstOrDefault(l => l.UserId == id);

            return Ok(categoryId);
        }

        [Route("{name}")]
        [HttpGet]
        public IActionResult GetByName(string? name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            var categoryName = _db.Users.FirstOrDefault(l => l.Username == name);
            if (categoryName == null)
            {

                return BadRequest();
            }

            return Ok(categoryName);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var deleteOrders = _db.Orders.Where(l => l.UserId == id).ToList();
            foreach (var order in deleteOrders) {

                _db.Orders.Remove(order);
            }
            var deleteUser = _db.Users.FirstOrDefault(c => c.UserId == id);

            _db.Users.Remove(deleteUser);
            _db.SaveChanges();
            return NoContent();
        }

    }
}
