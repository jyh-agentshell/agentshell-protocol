using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>实时连接诊断，不包含终端内容或凭据。</summary>
public sealed record ConnectionDiagnostics(
    [property: JsonPropertyName("host_id")] string HostId,
    [property: JsonPropertyName("is_online")] bool IsOnline,
    [property: JsonPropertyName("online_device_count")] int OnlineDeviceCount,
    [property: JsonPropertyName("last_active_at")] DateTimeOffset? LastActiveAt,
    [property: JsonPropertyName("disconnected_at")] DateTimeOffset? DisconnectedAt,
    [property: JsonPropertyName("realtime_enabled")] bool RealtimeEnabled);
