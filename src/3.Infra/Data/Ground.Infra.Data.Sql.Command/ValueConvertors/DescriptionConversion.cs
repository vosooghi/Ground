using Ground.Core.Domain.Toolkits.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ground.Infra.Data.Sql.Commands.ValueConvertors
{
    /// <summary>
    /// Value converter for Description value object to string.
    /// </summary>
    public class DescriptionConversion : ValueConverter<Description, string>
    {
        public DescriptionConversion() : base(c => c.Value, c => Description.FromString(c))
        {

        }
    }
}
