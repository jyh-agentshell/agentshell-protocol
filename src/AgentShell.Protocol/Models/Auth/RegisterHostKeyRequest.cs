using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

public sealed record RegisterHostKeyRequest(
    [property: JsonPropertyName("registration_token")][property: JsonRequired] string RegistrationToken,
    [property: JsonPropertyName("host_id")][property: JsonRequired] string HostId,
    [property: JsonPropertyName("public_key")][property: JsonRequired] string PublicKey);
