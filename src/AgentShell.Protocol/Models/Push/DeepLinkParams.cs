using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// P2.3 通知深链参数。
/// 格式：agentshell://host/{host_id}/session/{session_id}?action={action}
/// </summary>
public sealed class DeepLinkParams
{
    [JsonPropertyName("session_id")]
    public required string SessionId { get; init; }

    [JsonPropertyName("host_id")]
    public required string HostId { get; init; }

    [JsonPropertyName("agent_type")]
    public string? AgentType { get; init; }

    [JsonPropertyName("action")]
    public string Action { get; init; } = "terminal_takeover";
}
