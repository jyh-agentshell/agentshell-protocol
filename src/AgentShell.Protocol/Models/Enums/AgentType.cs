using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// Agent CLI 工具类型
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AgentType
{
    /// <summary>Codex CLI</summary>
    [JsonPropertyName("codex")]
    Codex,

    /// <summary>Claude Code</summary>
    [JsonPropertyName("claude")]
    Claude,

    /// <summary>OpenCode</summary>
    [JsonPropertyName("opencode")]
    OpenCode,

    /// <summary>aider</summary>
    [JsonPropertyName("aider")]
    Aider,

    /// <summary>未识别的 CLI 工具</summary>
    [JsonPropertyName("unknown")]
    Unknown,

    /// <summary>无 Agent 运行</summary>
    [JsonPropertyName("none")]
    None
}
