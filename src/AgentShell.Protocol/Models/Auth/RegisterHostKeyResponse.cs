using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

public sealed record RegisterHostKeyResponse(
    [property: JsonPropertyName("host_id")][property: JsonRequired] string HostId,
    [property: JsonPropertyName("registered")][property: JsonRequired] bool Registered);
