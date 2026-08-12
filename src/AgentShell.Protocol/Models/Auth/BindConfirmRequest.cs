using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// App 确认绑定。包含 daemon 的 Ed25519 签名及已预登记公钥。
/// 服务端只使用登记阶段可信的公钥验签，绑定请求不能登记或轮换密钥。
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
