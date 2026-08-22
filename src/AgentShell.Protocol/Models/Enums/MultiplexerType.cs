using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// 终端复用器类型
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<MultiplexerType>))]
public enum MultiplexerType
{
    /// <summary>tmux</summary>
    [JsonStringEnumMemberName("tmux")]
    Tmux,

    /// <summary>GNU Screen</summary>
    [JsonStringEnumMemberName("screen")]
    Screen,

    /// <summary>Zellij</summary>
    [JsonStringEnumMemberName("zellij")]
    Zellij,

    /// <summary>裸 PTY（无复用器）</summary>
    [JsonStringEnumMemberName("pty")]
    Pty
}
