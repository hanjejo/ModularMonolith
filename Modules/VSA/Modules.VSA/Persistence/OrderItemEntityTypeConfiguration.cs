using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.VSA.Domain;

namespace Modules.VSA.Persistence
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(e => e.Id)
                .HasConversion(new UlidValueConverter());
            builder.Property(e => e.OrderId)
                .HasConversion(new UlidValueConverter());
            builder.HasKey(e => e.Id);
        }
    }
}
