using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

public sealed record CreateRegistrationTokenResponse(
    [property: JsonPropertyName("registration_token")][property: JsonRequired] string RegistrationToken,
    [property: JsonPropertyName("expires_at")][property: JsonRequired] DateTimeOffset ExpiresAt);
