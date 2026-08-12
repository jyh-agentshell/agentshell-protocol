using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>P2.3 推送优先级（FCM/JPush 的传输提示，不携带业务内容）。</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PushPriority
{
    [JsonStringEnumMemberName("high")] High,
    [JsonStringEnumMemberName("normal")] Normal
}

/// <summary>
/// P2.3 最小隐私通知载荷。
/// 该对象是 FCM/JPush data payload 的唯一真相；不得添加终端输出、代码、命令、路径、diff 或凭据。
/// </summary>
public sealed record PushPayload(
    [property: JsonPropertyName("protocol_version")][property: JsonRequired] string ProtocolVersion,
    [property: JsonPropertyName("notification_id")][property: JsonRequired] string NotificationId,
    [property: JsonPropertyName("event_level")][property: JsonRequired] NotificationLevel EventLevel,
    [property: JsonPropertyName("host_id")][property: JsonRequired] string HostId,
    [property: JsonPropertyName("session_id")][property: JsonRequired] string SessionId,
    [property: JsonPropertyName("state")][property: JsonRequired] string State,
    [property: JsonPropertyName("occurred_at")][property: JsonRequired] DateTimeOffset OccurredAt,
    [property: JsonPropertyName("expires_at")][property: JsonRequired] DateTimeOffset ExpiresAt,
    [property: JsonPropertyName("collapse_key")][property: JsonRequired] string CollapseKey,
    [property: JsonPropertyName("deep_link")][property: JsonRequired] DeepLinkParams DeepLink)
{
    /// <summary>推送通道优先级，不属于线协议最小载荷。</summary>
    [JsonIgnore] public PushPriority Priority => EventLevel == NotificationLevel.Critical ? PushPriority.High : PushPriority.Normal;
}
