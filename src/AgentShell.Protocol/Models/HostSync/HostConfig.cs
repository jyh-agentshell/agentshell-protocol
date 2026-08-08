using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// SSH 认证方式
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AuthType
{
    [JsonPropertyName("password")]
    Password,

    [JsonPropertyName("private_key")]
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

    /// <summary>用户自定义显示名称</summary>
    [JsonPropertyName("label")]
    [JsonPropertyOrder(2)]
    public string? Label { get; init; }

    /// <summary>服务器地址</summary>
    [JsonPropertyName("hostname")]
    [JsonPropertyOrder(3)]
    public required string Hostname { get; init; }

    /// <summary>SSH 端口</summary>
    [JsonPropertyName("port")]
    [JsonPropertyOrder(4)]
    public int Port { get; init; } = 22;

    /// <summary>SSH 用户名</summary>
    [JsonPropertyName("username")]
    [JsonPropertyOrder(5)]
    public required string Username { get; init; }

    /// <summary>认证方式</summary>
    [JsonPropertyName("auth_type")]
    [JsonPropertyOrder(6)]
    public required AuthType AuthType { get; init; }

    /// <summary>AES-256-GCM 加密后的认证凭据（密码或私钥）</summary>
    [JsonPropertyName("encrypted_credential")]
    [JsonPropertyOrder(7)]
    public string? EncryptedCredential { get; init; }

    /// <summary>AES-256-GCM 加密 nonce</summary>
    [JsonPropertyName("encrypted_credential_nonce")]
    [JsonPropertyOrder(8)]
    public string? EncryptedCredentialNonce { get; init; }

    /// <summary>默认连接的会话名</summary>
    [JsonPropertyName("default_session")]
    [JsonPropertyOrder(9)]
    public string? DefaultSession { get; init; }

    /// <summary>终端复用器类型</summary>
    [JsonPropertyName("multiplexer_type")]
    [JsonPropertyOrder(10)]
    public MultiplexerType MultiplexerType { get; init; } = MultiplexerType.Tmux;

    /// <summary>用户自定义标签</summary>
    [JsonPropertyName("tags")]
    [JsonPropertyOrder(11)]
    public string[]? Tags { get; init; }
}
