using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>SignalR 订阅或重连后返回的主机完整状态快照。</summary>
public sealed record HostStateSnapshot(
    [property: JsonPropertyName("protocol_version")] string ProtocolVersion,
    [property: JsonPropertyName("host_id")] string HostId,
    [property: JsonPropertyName("snapshot_id")] Guid SnapshotId,
    [property: JsonPropertyName("sessions")] IReadOnlyList<SessionSnapshotItem> Sessions,
    [property: JsonPropertyName("server_timestamp")] DateTimeOffset ServerTimestamp);
