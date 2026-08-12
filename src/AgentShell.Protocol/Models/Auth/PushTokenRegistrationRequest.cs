using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>P2.3 设备向服务端登记或轮换厂商推送令牌的最小请求。</summary>
[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
public sealed class PushTokenRegistrationRequest
{
    /// <summary>推送厂商，仅允许 fcm 或 jpush。</summary>
    [JsonPropertyName("provider")]
    [JsonRequired]
    public required string Provider { get; init; }

    /// <summary>厂商签发的设备令牌；仅用于投递，响应和日志不得回显。</summary>
    [JsonPropertyName("token")]
    [JsonRequired]
    public required string Token { get; init; }

    [JsonPropertyName("app_version")]
    [JsonRequired]
    public required string AppVersion { get; init; }

    [JsonPropertyName("protocol_version")]
    [JsonRequired]
    public required string ProtocolVersion { get; init; }
}
