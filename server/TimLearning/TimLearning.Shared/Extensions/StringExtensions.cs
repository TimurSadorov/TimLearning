using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace TimLearning.Shared.Extensions;

public static class StringExtensions
{
    public static byte[] EncodeUTF8(this string str) => Encoding.UTF8.GetBytes(str);

    public static bool HasText([NotNullWhen(true)] this string? s)
    {
        return (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)) is false;
    }
}
