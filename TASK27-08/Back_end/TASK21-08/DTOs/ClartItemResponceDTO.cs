using TASK21_08.Models;

namespace TASK21_08.DTOs
{
    public class ClartItemResponceDTO
    {
        public int CartItemId { get; set; }

        public int? CartId { get; set; }


        public int Quantity { get; set; }

       

        public  productDTO? Product { get; set; }
    }

    public class productDTO
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public string? ProductImage { get; set; }

    }

}
