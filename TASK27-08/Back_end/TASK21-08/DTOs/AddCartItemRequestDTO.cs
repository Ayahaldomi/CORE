namespace TASK21_08.DTOs
{
    public class AddCartItemRequestDTO
    {
        public int? CartId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
