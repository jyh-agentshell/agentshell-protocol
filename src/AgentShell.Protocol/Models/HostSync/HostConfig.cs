using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// SSH 认证方式
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AuthType
{
    [JsonStringEnumMemberName("password")]
    Password,

    [JsonStringEnumMemberName("private_key")]
    PrivateKey
}

/// <summary>
/// 主机配置（客户端加密后上传的版本）
/// </summary>
public sealed class HostConfig
{
    /// <summary>主机唯一标识</summary>
    [JsonPropertyName("host_id")]
    [JsonPropertyOrder(1)]
    public required string HostId { get; init; }

    /// <summary>客户端加密后的完整主机配置密文</summary>
    [JsonPropertyName("ciphertext")]
    [JsonPropertyOrder(2)]
    public required string Ciphertext { get; init; }

    /// <summary>加密随机 nonce</summary>
    [JsonPropertyName("nonce")]
    [JsonPropertyOrder(3)]
    public required string Nonce { get; init; }

    /// <summary>附加认证数据（AAD）</summary>
    [JsonPropertyName("aad")]
    [JsonPropertyOrder(4)]
    public required string Aad { get; init; }

    /// <summary>客户端加密格式版本</summary>
    [JsonPropertyName("encryption_version")]
    [JsonPropertyOrder(5)]
    public required int EncryptionVersion { get; init; }

    /// <summary>主机配置最后更新时间</summary>
    [JsonPropertyName("updated_at")]
    [JsonPropertyOrder(6)]
    public required DateTimeOffset UpdatedAt { get; init; }
}
