using Ground.Core.Domain.Toolkits.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ground.Infra.Data.Sql.Commands.ValueConvertors
{
    /// <summary>
    /// Provides a value converter for mapping between the Title domain type and its string representation for use with
    /// Entity Framework Core.
    /// </summary>
    public class TitleConversion : ValueConverter<Title, string>
    {
        public TitleConversion() : base(c => c.Value, c => Title.FromString(c))
        {

        }
    }
}
