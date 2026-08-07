namespace AgentShell.Protocol.Models;

/// <summary>
/// 终端复用器类型
/// </summary>
public enum MultiplexerType
{
    /// <summary>tmux</summary>
    Tmux,

    /// <summary>GNU Screen</summary>
    Screen,

    /// <summary>Zellij</summary>
    Zellij,

    /// <summary>裸 PTY（无复用器）</summary>
    Pty
}
