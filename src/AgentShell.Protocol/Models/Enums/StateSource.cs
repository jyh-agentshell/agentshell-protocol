using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// 状态检测来源
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StateSource
{
    /// <summary>ANSI OSC 结构化标记（优先级高）</summary>
    [JsonPropertyName("osc_marker")]
    OscMarker,

    /// <summary>正则解析回退</summary>
    [JsonPropertyName("regex_fallback")]
    RegexFallback
}
