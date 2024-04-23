using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimLearning.Infrastructure.Implementation.Db.Conversions;
using TimLearning.Infrastructure.Implementation.Db.ValueComparers;

namespace TimLearning.Infrastructure.Implementation.Db.Extensions;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TProperty> HasJsonConversion<TProperty>(
        this PropertyBuilder<TProperty> builder
    )
    {
        return builder.HasConversion<JsonConversion<TProperty>>();
    }

    public static PropertyBuilder<List<TProperty>> HasListJsonConversion<TProperty>(
        this PropertyBuilder<List<TProperty>> builder
    )
        where TProperty : IEquatable<TProperty>
    {
        return builder.HasConversion<
            JsonConversion<List<TProperty>>,
            EnumerableValueComparer<TProperty>
        >();
    }
}
