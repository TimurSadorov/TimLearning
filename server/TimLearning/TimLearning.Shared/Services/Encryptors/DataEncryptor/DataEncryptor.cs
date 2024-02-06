using System.Security.Cryptography;
using System.Text.Json;
using TimLearning.Shared.Extensions;

namespace TimLearning.Shared.Services.Encryptors.DataEncryptor;

public class DataEncryptor : IDataEncryptor
{
    private readonly JsonSerializerOptions _dataJsonSerializerOptions;
    private readonly byte[] _key;

    public DataEncryptor(string key, JsonSerializerOptions? dataJsonSerializerOptions = null)
    {
        _dataJsonSerializerOptions = dataJsonSerializerOptions ?? new JsonSerializerOptions();
        _key = key.EncodeUTF8();
    }

    public string Sign(object data)
    {
        var serializedData = JsonSerializer.Serialize(data, _dataJsonSerializerOptions);
        var signature = HMACSHA256.HashData(_key, serializedData.EncodeUTF8());
        return Convert.ToHexString(signature);
    }

    public bool VerifySignature(string signature, object data)
    {
        var currentDataSignature = Sign(data);
        return currentDataSignature == signature;
    }
}
