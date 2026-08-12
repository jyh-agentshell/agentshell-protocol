using System.Globalization;

namespace AgentShell.Protocol.Models;

/// <summary>Protocol 线协议唯一允许的 UTC 毫秒时间格式。</summary>
public static class ProtocolTimestamp
{
    public const string FormatString = "yyyy-MM-dd'T'HH:mm:ss.fff'Z'";

    public static string Format(DateTimeOffset value) =>
        value.ToUniversalTime().ToString(FormatString, CultureInfo.InvariantCulture);

    public static bool TryParse(string? value, out DateTimeOffset timestamp)
    {
        if (value is null || !DateTimeOffset.TryParseExact(
                value,
                FormatString,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out timestamp))
        {
            timestamp = default;
            return false;
        }

        return string.Equals(value, Format(timestamp), StringComparison.Ordinal);
    }
}
