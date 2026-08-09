using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// 绑定确认响应。验签成功后返回 daemon 的首个 Access Token。
/// </summary>
public sealed class BindConfirmResponse
{
    /// <summary>绑定是否成功</summary>
    [JsonPropertyName("bound")]
    public bool Bound { get; init; }

    /// <summary>主机 ID（host_id）</summary>
    [JsonPropertyName("host_id")]
    [JsonRequired]
    public required string HostId { get; init; }

    /// <summary>首个 Access Token（daemon 用于后续上报和续期）</summary>
    [JsonPropertyName("access_token")]
    [JsonRequired]
    public required string AccessToken { get; init; }

    /// <summary>Token 类型</summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; } = "Bearer";

    /// <summary>过期秒数</summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; } = 3600;
}
