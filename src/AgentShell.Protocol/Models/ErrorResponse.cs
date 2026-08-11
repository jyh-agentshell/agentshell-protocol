using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

public sealed record ErrorResponse(
    [property: JsonPropertyName("error")] string Error,
    [property: JsonPropertyName("code")] RegistrationErrorCode Code);
