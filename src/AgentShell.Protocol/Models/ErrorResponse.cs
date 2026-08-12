using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

public sealed record ErrorResponse(
    [property: JsonPropertyName("error")][property: JsonRequired] string Error,
    [property: JsonPropertyName("code")][property: JsonRequired] RegistrationErrorCode Code);
