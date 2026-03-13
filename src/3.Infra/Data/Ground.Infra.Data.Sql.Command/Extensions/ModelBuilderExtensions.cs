using Ground.Core.Domain.Entities;
using Ground.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ground.Infra.Data.Sql.Commands.Extensions
{
    /// <summary>
    /// An extension class for the ModelBuilder to provide methods for adding a BusinessId property to entities that inherit from AggregateRoot or Entity, and for applying value converters to properties of a specific type.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        public static void AddBusinessId(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model
                                                   .GetEntityTypes()
                                                   .Where(e => typeof(AggregateRoot).IsAssignableFrom(e.ClrType) ||
                                                        typeof(Entity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<BusinessId>("BusinessId").HasConversion(c => c.Value, d => BusinessId.FromGuid(d))
                    .IsUnicode()
                    .IsRequired();
                modelBuilder.Entity(entityType.ClrType).HasAlternateKey("BusinessId");
            }
        }
        public static ModelBuilder UseValueConverterForType<T>(this ModelBuilder modelBuilder, ValueConverter converter, int maxLenght = 0)
        {
            return modelBuilder.UseValueConverterForType(typeof(T), converter, maxLenght);
        }
        public static ModelBuilder UseValueConverterForType(this ModelBuilder modelBuilder, Type type, ValueConverter converter, int maxLength = 0)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == type);

                foreach (var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion(converter);
                    if (maxLength > 0)
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasMaxLength(maxLength);
                }
            }

            return modelBuilder;
        }
    }

}
