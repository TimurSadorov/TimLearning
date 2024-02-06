using System.Text;

namespace TimLearning.Shared.Extensions;

public static class StringExtensions
{
    public static byte[] EncodeUTF8(this string str) =>
        Encoding.UTF8.GetBytes(str);
}