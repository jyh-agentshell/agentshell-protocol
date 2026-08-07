namespace AgentShell.Protocol.Models;

/// <summary>
/// Agent CLI 工具类型
/// </summary>
public enum AgentType
{
    /// <summary>Codex CLI</summary>
    Codex,

    /// <summary>Claude Code</summary>
    Claude,

    /// <summary>OpenCode</summary>
    OpenCode,

    /// <summary>aider</summary>
    Aider,

    /// <summary>未识别的 CLI 工具</summary>
    Unknown,

    /// <summary>无 Agent 运行</summary>
    None
}
