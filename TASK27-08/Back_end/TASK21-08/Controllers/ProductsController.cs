using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TASK21_08.DTOs;
using TASK21_08.Models;

namespace TASK21_08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;
        public ProductsController(MyDbContext db) { _db = db; }


        //[HttpGet("number")]
        //public IActionResult number(int num1, int num2)
        //{

        //    if (num1 == 30 || num2 == 30)
        //    {

        //        return Ok("true");
        //    }
        //    if (num1 + num2 == 30)
        //    {
        //        return Ok("true");
        //    }
        //    return Ok("false");
        //}

        //[HttpGet("three")]
        //public IActionResult three(int num1)
        //{
        //    if (num1 % 3 == 0 || num1 % 7 == 0)
        //    {
        //        return Ok("true");
        //    }
        //    return Ok("false");
        //}

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var product = _db.Products.ToList();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> postProduct([FromForm] productPost product)
        {
            if (product.ProductImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var filePath = Path.Combine(uploadsFolderPath, product.ProductImage.FileName);

                //Ensure the file stream is properly handled with async/ await
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ProductImage.CopyToAsync(stream);
                }
            }

            var pro = new Product
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ProductImage = product.ProductImage?.FileName // Using ?. to avoid null reference if no image is uploaded
            };

            _db.Products.Add(pro);
            await _db.SaveChangesAsync(); // Use async save changes
            return Ok(pro);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> putProduct(int id, [FromForm] productPost product)
        {
            if (product.ProductImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var filePath = Path.Combine(uploadsFolderPath, product.ProductImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ProductImage.CopyToAsync(stream);
                }

            }
            var x = _db.Products.FirstOrDefault(l => l.ProductId == id);


            x.ProductName = product.ProductName;
            x.Description = product.Description;
            x.Price = product.Price;
            x.CategoryId = product.CategoryId;
            x.ProductImage = product.ProductImage.FileName;

            _db.Products.Update(x);

            _db.SaveChanges();
            return Ok(x);
        }

        [Route("sort")]
        [HttpGet]
        public IActionResult sort()
        {

            var product = _db.Products.OrderByDescending(l => l.Price).ToList();



            return Ok(product);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public IActionResult GetById(int id)
        {
            if (id < 0)
            {

                return BadRequest();
            }

            var product = _db.Products.Where(l => l.ProductId == id).FirstOrDefault();

            if (product == null)
            {

                return NotFound($"the product with id of {id} is Not Found");
            }

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


        [Authorize]
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
        public IActionResult GetByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            var productName = _db.Products.Where(l => l.ProductName == name);
            if (productName == null)
            {
                return NotFound();


            }
            return Ok(productName);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
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
        [HttpGet("orderby")]
        public IActionResult sortProduct()
        {
            var prodct = _db.Products.OrderBy(l => l.ProductName).Reverse().ToList();
            var p = new List<Product>();
            for (var i = 0; i < 5; i++)
            {
                p.Add(prodct[i]);
            }
            p.Reverse();
            return Ok(p);

        }



        // POST: api/Products/category/5
        [HttpPost("category/{categoryId:int}")]
        public async Task<ActionResult> AddProductToCategory(int categoryId, [FromForm] productPost productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is null");
            }

            var imageName = await UploadImageAsync(productDto); //here we call the methed that we made before , pass ur DTO object here.

            // this code will upload the image to the server then it will store the full path in the databse so u can use it in the front end 

            var product = new Product
            {
                CategoryId = categoryId,
                Description = productDto.Description,
                Price = productDto.Price,
                ProductName = productDto.ProductName,
                ProductImage = imageName // this is the full path to the image from the server
            };

            _db.Products.Add(product); //here we add it to the database 
            await _db.SaveChangesAsync();

            return Ok(product);
        }

        // Helper method to upload images
        private async Task<string> UploadImageAsync(productPost dto) //alter this depending on your DTO
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images"); // Images is the name of the Imagse folder 

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var imageFile = Path.Combine(uploadFolder, dto.ProductImage.FileName); // here we get the file name to combine it with the folder path

            using (var stream = new FileStream(imageFile, FileMode.Create))
            {
                await dto.ProductImage.CopyToAsync(stream);
            }

            // Return the full URL to the image
            var imageUrl = $"{Request.Scheme}://{Request.Host}/Images/{dto.ProductImage.FileName}"; // this is the full path from the server , you can use it in the front end 
            return imageUrl; //this method returns the full url of the image , store this in the table as a image path , then use it in your front end 
        }


    }
}
