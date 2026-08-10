using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// 推送通知类型
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PushType
{
    [JsonStringEnumMemberName("approval_required")]
    ApprovalRequired,

    [JsonStringEnumMemberName("agent_error")]
    AgentError,

    [JsonStringEnumMemberName("session_ended")]
    SessionEnded,

    [JsonStringEnumMemberName("daemon_update_available")]
    DaemonUpdateAvailable,

    [JsonStringEnumMemberName("binding_request")]
    BindingRequest
}

/// <summary>
/// 推送优先级
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PushPriority
{
    [JsonStringEnumMemberName("high")]
    High,

    [JsonStringEnumMemberName("normal")]
    Normal
}

/// <summary>
/// FCM/JPush 推送通知载荷
/// </summary>
public sealed class PushPayload
{
    /// <summary>通知唯一标识</summary>
    [JsonPropertyName("notification_id")]
    [JsonPropertyOrder(1)]
    public required string NotificationId { get; init; }

    /// <summary>时间戳（UTC）</summary>
    [JsonPropertyName("timestamp")]
    [JsonPropertyOrder(2)]
    public required DateTimeOffset Timestamp { get; init; }

    /// <summary>通知类型</summary>
    [JsonPropertyName("type")]
    [JsonPropertyOrder(3)]
    public required PushType Type { get; init; }

    /// <summary>通知标题</summary>
    [JsonPropertyName("title")]
    [JsonPropertyOrder(4)]
    public string? Title { get; init; }

    /// <summary>通知正文</summary>
    [JsonPropertyName("body")]
    [JsonPropertyOrder(5)]
    public string? Body { get; init; }

    /// <summary>点击通知后传递给 App 的数据</summary>
    [JsonPropertyName("data")]
    [JsonPropertyOrder(6)]
    public PushData? Data { get; init; }

    /// <summary>推送优先级</summary>
    [JsonPropertyName("priority")]
    [JsonPropertyOrder(7)]
    public PushPriority Priority { get; init; } = PushPriority.High;

    /// <summary>通知存活时间（秒）</summary>
    [JsonPropertyName("ttl_seconds")]
    [JsonPropertyOrder(8)]
    public int TtlSeconds { get; init; } = 300;

    /// <summary>P2.3：事件分级</summary>
    [JsonPropertyName("event_level")]
    [JsonPropertyOrder(9)]
    public NotificationLevel? EventLevel { get; init; }

    /// <summary>P2.3：通知去重折叠键（同 host 同类型通知会被后来的替换）</summary>
    [JsonPropertyName("collapse_key")]
    [JsonPropertyOrder(10)]
    public string? CollapseKey { get; init; }
}

/// <summary>
/// 推送数据载荷
/// </summary>
public sealed class PushData
{
    [JsonPropertyName("session_id")]
    public string? SessionId { get; init; }

    [JsonPropertyName("host_id")]
    public string? HostId { get; init; }

    [JsonPropertyName("agent_type")]
    public string? AgentType { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("file_count")]
    public int? FileCount { get; init; }

    [JsonPropertyName("action_url")]
    public string? ActionUrl { get; init; }
}
