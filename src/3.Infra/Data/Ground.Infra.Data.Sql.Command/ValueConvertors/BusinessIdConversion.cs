using Ground.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ground.Infra.Data.Sql.Commands.ValueConvertors
{
    /// <summary>
    /// Value converter for BusinessId value object to Guid.
    /// </summary>
    public class BusinessIdConversion : ValueConverter<BusinessId, Guid>
    {
        public BusinessIdConversion() : base(c => c.Value, c => BusinessId.FromGuid(c))
        {

        }
    }
}
