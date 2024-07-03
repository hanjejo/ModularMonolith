using SharedKernel;

namespace Modules.VSA.Domain
{
    public class OrderItem : Entity<Ulid>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Ulid OrderId { get; set; }
        public Order Order { get; set; }
    }
}

