using TASK21_08.Models;

namespace TASK21_08.DTOs
{
    public class productPost
    {
       

        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? CategoryId { get; set; }

        public IFormFile? ProductImage { get; set; }

       
    }
}
