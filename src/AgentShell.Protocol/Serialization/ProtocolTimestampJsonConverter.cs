using System.Text.Json;
using System.Text.Json.Serialization;
using AgentShell.Protocol.Models;

namespace AgentShell.Protocol.Serialization;

/// <summary>拒绝非 UTC 毫秒精度时间戳，避免签名字节出现跨实现歧义。</summary>
public sealed class ProtocolTimestampJsonConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String || !ProtocolTimestamp.TryParse(reader.GetString(), out var timestamp))
            throw new JsonException("时间戳必须使用 UTC 毫秒格式。");

        return timestamp;
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) =>
        writer.WriteStringValue(ProtocolTimestamp.Format(value));
}
