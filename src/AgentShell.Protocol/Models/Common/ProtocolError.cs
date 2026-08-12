using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>状态链路的稳定错误载荷，不包含敏感实现细节。</summary>
public sealed record ProtocolError(
    [property: JsonPropertyName("code")] string Code,
    [property: JsonPropertyName("message")] string? Message = null,
    [property: JsonPropertyName("protocol_min")] string? ProtocolMinimum = null,
    [property: JsonPropertyName("protocol_max")] string? ProtocolMaximum = null);
