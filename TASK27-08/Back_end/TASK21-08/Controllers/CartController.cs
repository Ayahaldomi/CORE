using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TASK21_08.DTOs;
using TASK21_08.Models;

namespace TASK21_08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CartController(MyDbContext db) { _db = db; }

        [HttpGet]
        public IActionResult Get() { 
            var allcarts = _db.CartItems.ToList();
            return Ok(allcarts);
        }


        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetAll (int id)
        {
            var getData = _db.CartItems.Where(l => l.CartId == id).Select(x =>
            new ClartItemResponceDTO
            {
                CartId = x.CartId,
                CartItemId = x.CartItemId,
                Quantity = x.Quantity,
         
                Product = new productDTO
                {
                    ProductId = x.Product.ProductId,
                    Price = x.Product.Price,
                    Description = x.Product.Description,
                    ProductName = x.Product.ProductName,
                    ProductImage = x.Product.ProductImage,
                }
            }
            ).ToList();

            return Ok(getData);
        }
        [HttpPost]
        public IActionResult addCartItems([FromBody] AddCartItemRequestDTO CART)
        {
            var data = new CartItem
            {
                CartId = CART.CartId,
                Quantity = CART.Quantity,
                ProductId = CART.ProductId,

            };
            _db.CartItems.Add(data);
            _db.SaveChanges();

            return Ok();


        }

        [HttpDelete("{id}")]
        public IActionResult deleteCartItem(int id) { 

            var cartItem = _db.CartItems.FirstOrDefault(l => l.CartItemId == id);

            _db.CartItems.Remove(cartItem);
            _db.SaveChanges();
            return Ok();
            
        }

        [HttpPut("{id}")]
        public IActionResult cartItemPut(int id, [FromBody] cartItemPutDTO cartItem) { 
            var cart = _db.CartItems.FirstOrDefault(l =>l.CartItemId == id);
            cart.Quantity = cartItem.Quantity;
            _db.CartItems.Update(cart);
            _db.SaveChanges();
            return Ok();
        }
    }
}
