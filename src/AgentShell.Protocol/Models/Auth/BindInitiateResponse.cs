using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// Server 对绑定发起的响应。返回供 daemon 签名的 nonce。
/// </summary>
public sealed class BindInitiateResponse
{
    /// <summary>绑定会话 ID（服务端生成，用于关联 initiate 和 confirm）</summary>
    [JsonPropertyName("challenge_id")]
    [JsonRequired]
    public required string ChallengeId { get; init; }

    /// <summary>32 字节随机 nonce（Base64 编码），daemon 需签名 "{binding_code}:{nonce}"</summary>
    [JsonPropertyName("nonce")]
    [JsonRequired]
    public required string Nonce { get; init; }

    /// <summary>nonce 有效期（秒）</summary>
    [JsonPropertyName("ttl_seconds")]
    public int TtlSeconds { get; init; } = 300;
}
