using SharedKernel;

namespace Modules.VSA.Domain
{
    public class Order : AggregateRoot<Ulid>
    {
        public string Name { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public void AddOrderItem(OrderItem item)
        {
            OrderItems.Add(item);
        }

        public void UpdateOrderItemQuantity(Ulid orderItemId, int quantity)
        {
            var orderItem = OrderItems.First(o => o.Id == orderItemId);
            orderItem.Quantity = quantity;
        }
    }
}

