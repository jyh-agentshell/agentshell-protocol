using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// Agent CLI 工具类型
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AgentType
{
    /// <summary>Codex CLI</summary>
    [JsonStringEnumMemberName("codex")]
    Codex,

    /// <summary>Claude Code</summary>
    [JsonStringEnumMemberName("claude")]
    Claude,

    /// <summary>OpenCode</summary>
    [JsonStringEnumMemberName("opencode")]
    OpenCode,

    /// <summary>aider</summary>
    [JsonStringEnumMemberName("aider")]
    Aider,

    /// <summary>未识别的 CLI 工具</summary>
    [JsonStringEnumMemberName("unknown")]
    Unknown,

    /// <summary>无 Agent 运行</summary>
    [JsonStringEnumMemberName("none")]
    None
}
