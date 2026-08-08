using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// 会话生命周期事件类型
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SessionEventType
{
    [JsonPropertyName("session_created")]
    Created,

    [JsonPropertyName("session_attached")]
    Attached,

    [JsonPropertyName("session_detached")]
    Detached,

    [JsonPropertyName("session_destroyed")]
    Destroyed
}

/// <summary>
/// 守护进程上报的会话生命周期事件
/// </summary>
public sealed class SessionLifecycleEvent
{
    /// <summary>事件唯一标识</summary>
    [JsonPropertyName("event_id")]
    [JsonPropertyOrder(1)]
    public required string EventId { get; init; }

    /// <summary>事件发生时间（UTC）</summary>
    [JsonPropertyName("timestamp")]
    [JsonPropertyOrder(2)]
    public required DateTimeOffset Timestamp { get; init; }

    /// <summary>会话标识</summary>
    [JsonPropertyName("session_id")]
    [JsonPropertyOrder(3)]
    public required string SessionId { get; init; }

    /// <summary>生命周期事件类型</summary>
    [JsonPropertyName("event_type")]
    [JsonPropertyOrder(4)]
    public required SessionEventType EventType { get; init; }

    /// <summary>终端复用器类型</summary>
    [JsonPropertyName("multiplexer_type")]
    [JsonPropertyOrder(5)]
    public required MultiplexerType MultiplexerType { get; init; }

    /// <summary>复用器内的会话名</summary>
    [JsonPropertyName("session_name")]
    [JsonPropertyOrder(6)]
    public string? SessionName { get; init; }

    /// <summary>会话中检测到的 Agent 类型</summary>
    [JsonPropertyName("agent_type")]
    [JsonPropertyOrder(7)]
    public AgentType AgentType { get; init; } = AgentType.None;

    /// <summary>窗格数量</summary>
    [JsonPropertyName("pane_count")]
    [JsonPropertyOrder(8)]
    public int? PaneCount { get; init; }

    /// <summary>守护进程版本号</summary>
    [JsonPropertyName("daemon_version")]
    [JsonPropertyOrder(9)]
    public string? DaemonVersion { get; init; }
}
