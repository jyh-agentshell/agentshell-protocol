using System.Text.Json;
using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// WebSocket 消息类型
/// </summary>
public enum WsMessageType
{
    [JsonPropertyName("agent_state_changed")]
    AgentStateChanged,

    [JsonPropertyName("session_lifecycle")]
    SessionLifecycle,

    [JsonPropertyName("approval_action")]
    ApprovalAction,

    [JsonPropertyName("heartbeat")]
    Heartbeat,

    [JsonPropertyName("error")]
    Error,

    [JsonPropertyName("daemon_status")]
    DaemonStatus,

    [JsonPropertyName("binding_challenge")]
    BindingChallenge,

    [JsonPropertyName("binding_response")]
    BindingResponse
}

/// <summary>
/// 消息方向
/// </summary>
public enum MessageDirection
{
    [JsonPropertyName("server_to_client")]
    ServerToClient,

    [JsonPropertyName("client_to_server")]
    ClientToServer
}

/// <summary>
/// SignalR WebSocket 消息信封
/// </summary>
public sealed class WsMessage
{
    /// <summary>消息唯一标识</summary>
    [JsonPropertyName("message_id")]
    [JsonPropertyOrder(1)]
    public required string MessageId { get; init; }

    /// <summary>时间戳（UTC）</summary>
    [JsonPropertyName("timestamp")]
    [JsonPropertyOrder(2)]
    public required DateTimeOffset Timestamp { get; init; }

    /// <summary>消息类型</summary>
    [JsonPropertyName("type")]
    [JsonPropertyOrder(3)]
    public required WsMessageType Type { get; init; }

    /// <summary>消息方向</summary>
    [JsonPropertyName("direction")]
    [JsonPropertyOrder(4)]
    public MessageDirection Direction { get; init; }

    /// <summary>消息内容（JSON object）</summary>
    [JsonPropertyName("payload")]
    [JsonPropertyOrder(5)]
    public required JsonElement Payload { get; init; }

    /// <summary>关联 ID（请求-响应配对）</summary>
    [JsonPropertyName("correlation_id")]
    [JsonPropertyOrder(6)]
    public string? CorrelationId { get; init; }
}
