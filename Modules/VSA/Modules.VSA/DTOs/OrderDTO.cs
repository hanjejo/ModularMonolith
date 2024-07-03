namespace Modules.VSA.DTOs
{
    public class OrderDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }

}
