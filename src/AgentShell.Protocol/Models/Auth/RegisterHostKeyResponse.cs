using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

public sealed record RegisterHostKeyResponse(
    [property: JsonPropertyName("host_id")] string HostId,
    [property: JsonPropertyName("registered")] bool Registered);
