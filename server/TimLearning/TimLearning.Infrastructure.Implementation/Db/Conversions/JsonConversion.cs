using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TimLearning.Infrastructure.Implementation.Db.Conversions;

public class JsonConversion<T> : ValueConverter<T, string>
{
    // ReSharper disable once StaticMemberInGenericType
    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new() { WriteIndented = false };

    public JsonConversion()
        : base(
            i => JsonSerializer.Serialize(i, JsonSerializerOptions),
            j => JsonSerializer.Deserialize<T>(j, JsonSerializerOptions)!
        ) { }
}
