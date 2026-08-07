namespace AgentShell.Protocol.Models;

/// <summary>
/// 状态检测来源
/// </summary>
public enum StateSource
{
    /// <summary>ANSI OSC 结构化标记（优先级高）</summary>
    OscMarker,

    /// <summary>正则解析回退</summary>
    RegexFallback
}
