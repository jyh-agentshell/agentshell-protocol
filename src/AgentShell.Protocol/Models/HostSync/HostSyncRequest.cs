using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// 同步操作类型
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SyncOperation
{
    [JsonStringEnumMemberName("full_sync")]
    FullSync,

    [JsonStringEnumMemberName("add")]
    Add,

    [JsonStringEnumMemberName("update")]
    Update,

    [JsonStringEnumMemberName("delete")]
    Delete
}

/// <summary>
/// 同步状态
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SyncStatus
{
    [JsonStringEnumMemberName("ok")]
    Ok,

    [JsonStringEnumMemberName("partial")]
    Partial,

    [JsonStringEnumMemberName("conflict")]
    Conflict,

    [JsonStringEnumMemberName("error")]
    Error
}

/// <summary>
/// 主机配置同步请求
/// </summary>
public sealed class SyncRequest
{
    [JsonPropertyName("operation")]
    [JsonPropertyOrder(1)]
    public required SyncOperation Operation { get; init; }

    [JsonPropertyName("hosts")]
    [JsonPropertyOrder(2)]
    public HostConfig[] Hosts { get; init; } = [];

    [JsonPropertyName("device_id")]
    [JsonPropertyOrder(3)]
    public string? DeviceId { get; init; }

    [JsonPropertyName("sync_timestamp")]
    [JsonPropertyOrder(4)]
    public DateTimeOffset? SyncTimestamp { get; init; }
}

/// <summary>
/// 冲突详情
/// </summary>
public sealed class SyncConflict
{
    [JsonPropertyName("host_id")]
    public required string HostId { get; init; }

    [JsonPropertyName("reason")]
    public string? Reason { get; init; }

    [JsonPropertyName("server_version")]
    public HostConfig? ServerVersion { get; init; }

    [JsonPropertyName("client_version")]
    public HostConfig? ClientVersion { get; init; }
}

/// <summary>
/// 主机配置同步响应
/// </summary>
public sealed class SyncResponse
{
    [JsonPropertyName("status")]
    [JsonPropertyOrder(1)]
    public required SyncStatus Status { get; init; }

    [JsonPropertyName("server_timestamp")]
    [JsonPropertyOrder(2)]
    public required DateTimeOffset ServerTimestamp { get; init; }

    [JsonPropertyName("conflicts")]
    [JsonPropertyOrder(3)]
    public SyncConflict[]? Conflicts { get; init; }

    [JsonPropertyName("message")]
    [JsonPropertyOrder(4)]
    public string? Message { get; init; }
}
