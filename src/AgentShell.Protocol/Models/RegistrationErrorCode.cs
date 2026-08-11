using System.Text.Json;
using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

[JsonConverter(typeof(RegistrationErrorCodeJsonConverter))]
public enum RegistrationErrorCode
{
    [JsonStringEnumMemberName("registration_token_invalid")]
    RegistrationTokenInvalid,

    [JsonStringEnumMemberName("registration_token_expired")]
    RegistrationTokenExpired,

    [JsonStringEnumMemberName("registration_token_consumed")]
    RegistrationTokenConsumed,

    [JsonStringEnumMemberName("host_key_conflict")]
    HostKeyConflict,

    [JsonStringEnumMemberName("host_key_invalid")]
    HostKeyInvalid,

    [JsonStringEnumMemberName("unauthorized")]
    Unauthorized,

    [JsonStringEnumMemberName("rate_limited")]
    RateLimited
}

public sealed class RegistrationErrorCodeJsonConverter : JsonConverter<RegistrationErrorCode>
{
    public override RegistrationErrorCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("注册错误码必须为字符串。");
        }

        return reader.GetString() switch
        {
            "registration_token_invalid" => RegistrationErrorCode.RegistrationTokenInvalid,
            "registration_token_expired" => RegistrationErrorCode.RegistrationTokenExpired,
            "registration_token_consumed" => RegistrationErrorCode.RegistrationTokenConsumed,
            "host_key_conflict" => RegistrationErrorCode.HostKeyConflict,
            "host_key_invalid" => RegistrationErrorCode.HostKeyInvalid,
            "unauthorized" => RegistrationErrorCode.Unauthorized,
            "rate_limited" => RegistrationErrorCode.RateLimited,
            _ => throw new JsonException("未知注册错误码。")
        };
    }

    public override void Write(Utf8JsonWriter writer, RegistrationErrorCode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            RegistrationErrorCode.RegistrationTokenInvalid => "registration_token_invalid",
            RegistrationErrorCode.RegistrationTokenExpired => "registration_token_expired",
            RegistrationErrorCode.RegistrationTokenConsumed => "registration_token_consumed",
            RegistrationErrorCode.HostKeyConflict => "host_key_conflict",
            RegistrationErrorCode.HostKeyInvalid => "host_key_invalid",
            RegistrationErrorCode.Unauthorized => "unauthorized",
            RegistrationErrorCode.RateLimited => "rate_limited",
            _ => throw new JsonException("未知注册错误码。")
        });
    }
}
