using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// daemon 续期响应。
/// </summary>
public sealed class RenewResponse
{
    /// <summary>新签发的 Access Token</summary>
    [JsonPropertyName("access_token")]
    [JsonRequired]
    public required string AccessToken { get; init; }

    /// <summary>Token 类型（固定 "Bearer"）</summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; } = "Bearer";

    /// <summary>过期秒数（3600）</summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; } = 3600;
}
