using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

public sealed record RegisterHostKeyRequest(
    [property: JsonPropertyName("registration_token")] string RegistrationToken,
    [property: JsonPropertyName("host_id")] string HostId,
    [property: JsonPropertyName("public_key")] string PublicKey);
