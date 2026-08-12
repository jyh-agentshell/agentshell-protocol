using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>服务端返回的单个会话状态快照。</summary>
public sealed record SessionSnapshotItem(
    [property: JsonPropertyName("session_id")] string SessionId,
    [property: JsonPropertyName("state")] AgentState State,
    [property: JsonPropertyName("agent_type")] AgentType AgentType,
    [property: JsonPropertyName("previous_state")] AgentState? PreviousState,
    [property: JsonPropertyName("timestamp")] DateTimeOffset Timestamp,
    [property: JsonPropertyName("daemon_version")] string DaemonVersion);

/// <summary>P2.1 会话快照响应及协议兼容信息。</summary>
public sealed record SessionSnapshotResponse(
    [property: JsonPropertyName("sessions")] IReadOnlyList<SessionSnapshotItem> Sessions,
    [property: JsonPropertyName("server_timestamp")] DateTimeOffset ServerTimestamp,
    [property: JsonPropertyName("realtime_enabled")] bool RealtimeEnabled,
    [property: JsonPropertyName("feature_enabled")] bool FeatureEnabled,
    [property: JsonPropertyName("protocol_min")] string ProtocolMinimum,
    [property: JsonPropertyName("protocol_max")] string ProtocolMaximum);
