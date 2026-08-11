using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
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
