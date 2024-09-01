﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //[HttpPost("math")]
        //public IActionResult math(string math)
        //{
        //   string[] m = math.Split(' ');
        //    if (Convert.ToChar(m[1]) == '-') {
        //        var Result = Convert.ToInt64(m[0]) - Convert.ToInt64(m[2]);
        //        return Ok(Result);
        //    }
        //    else if (Convert.ToChar(m[1]) == '+') {
        //        var Result = Convert.ToInt64(m[0]) + Convert.ToInt64(m[2]);
        //        return Ok(Result);
        //    }
        //    return BadRequest();

        //}
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
            foreach (var order in deleteOrders)
            {

                _db.Orders.Remove(order);
            }
            var deleteUser = _db.Users.FirstOrDefault(c => c.UserId == id);

            _db.Users.Remove(deleteUser);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromForm] usersDTOs model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            byte[] passwordHash, passwordSalt;
            PasswordHasher.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
            User user = new User
            {
                Username = "",
                Password = model.Password,
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            //For Demo Purpose we are returning the PasswordHash and PasswordSalt
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromForm]usersDTOs model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null || !PasswordHasher.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid username or password.");
            }
            // Generate a token or return a success response
            return Ok("User logged in successfully");
        }

    }
}
