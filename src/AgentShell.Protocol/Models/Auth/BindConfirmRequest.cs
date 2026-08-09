using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// App 确认绑定。包含 daemon 的 Ed25519 签名及公钥。
/// 公钥由此首次上传到 Server，作为后续续期验签的根信任。
/// </summary>
public sealed class BindConfirmRequest
{
    /// <summary>绑定会话 ID（来自 BindInitiateResponse）</summary>
    [JsonPropertyName("challenge_id")]
    [JsonRequired]
    [JsonPropertyOrder(1)]
    public required string ChallengeId { get; init; }

    /// <summary>6 位绑定码</summary>
    [JsonPropertyName("binding_code")]
    [JsonRequired]
    [JsonPropertyOrder(2)]
    public required string BindingCode { get; init; }

    /// <summary>daemon 的主机 ID（来自 bind-verify CLI 输出）</summary>
    [JsonPropertyName("host_id")]
    [JsonRequired]
    [JsonPropertyOrder(3)]
    public required string HostId { get; init; }

    /// <summary>daemon 的 Ed25519 签名，对 "{binding_code}:{nonce}" 的 Base64 编码</summary>
    [JsonPropertyName("signature")]
    [JsonRequired]
    [JsonPropertyOrder(4)]
    public required string Signature { get; init; }

    /// <summary>daemon 的 Ed25519 公钥（Base64 编码，32 字节）</summary>
    [JsonPropertyName("public_key")]
    [JsonRequired]
    [JsonPropertyOrder(5)]
    public required string PublicKey { get; init; }
}
