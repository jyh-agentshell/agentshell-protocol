using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// 守护进程上报的 Agent 状态变化事件
/// </summary>
public sealed class AgentStateEvent
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

    /// <summary>Agent CLI 工具类型</summary>
    [JsonPropertyName("agent_type")]
    [JsonPropertyOrder(4)]
    public required AgentType AgentType { get; init; }

    /// <summary>Agent 当前状态</summary>
    [JsonPropertyName("state")]
    [JsonPropertyOrder(5)]
    public required AgentState State { get; init; }

    /// <summary>状态变更前的上一个状态</summary>
    [JsonPropertyName("previous_state")]
    [JsonPropertyOrder(6)]
    public AgentState? PreviousState { get; init; }

    /// <summary>状态详细信息</summary>
    [JsonPropertyName("detail")]
    [JsonPropertyOrder(7)]
    public AgentStateDetail? Detail { get; init; }

    /// <summary>状态信息来源</summary>
    [JsonPropertyName("source")]
    [JsonPropertyOrder(8)]
    public StateSource? Source { get; init; }

    /// <summary>协议版本号</summary>
    [JsonPropertyName("protocol_version")]
    [JsonPropertyOrder(9)]
    public string? ProtocolVersion { get; init; }

    /// <summary>守护进程版本号</summary>
    [JsonPropertyName("daemon_version")]
    [JsonPropertyOrder(10)]
    public string? DaemonVersion { get; init; }
}
