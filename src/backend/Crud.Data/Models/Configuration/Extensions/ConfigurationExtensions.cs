using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud.Data.Models.Configuration.Extensions;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Configure a property to follow sensible, default string rules for SQL string columns.
    /// Enforces:
    ///     - Required
    ///     - Max length = 128
    /// </summary>
    public static PropertyBuilder<TProperty> SensibleRequiredString<TEntity, TProperty>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, TProperty>> accessor)
        where TEntity : class
    {
        return builder
            .Property(accessor)
            .HasMaxLength(128)
            .IsRequired();
    }
}