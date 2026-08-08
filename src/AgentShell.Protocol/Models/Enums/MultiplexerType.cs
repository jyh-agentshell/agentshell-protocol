using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// 终端复用器类型
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MultiplexerType
{
    /// <summary>tmux</summary>
    [JsonPropertyName("tmux")]
    Tmux,

    /// <summary>GNU Screen</summary>
    [JsonPropertyName("screen")]
    Screen,

    /// <summary>Zellij</summary>
    [JsonPropertyName("zellij")]
    Zellij,

    /// <summary>裸 PTY（无复用器）</summary>
    [JsonPropertyName("pty")]
    Pty
}
