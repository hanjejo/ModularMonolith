using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Modules.VSA.Persistence
{
    public class UlidValueConverter : ValueConverter<Ulid, string>
    {
        public UlidValueConverter()
            : base(
                  ulid => ulid.ToString(),
                  str => Ulid.Parse(str))
        {
        }
    }
}
