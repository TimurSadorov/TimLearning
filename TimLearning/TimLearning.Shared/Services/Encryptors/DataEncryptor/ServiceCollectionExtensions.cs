using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TimLearning.Shared.Services.Encryptors.DataEncryptor;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataHasher(
        this IServiceCollection services,
        string key,
        JsonSerializerOptions? dataJsonSerializerOptions = null
    )
    {
        services.TryAddSingleton<IDataEncryptor>(new DataEncryptor(key, dataJsonSerializerOptions));

        return services;
    }
}
