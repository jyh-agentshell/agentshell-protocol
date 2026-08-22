using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// 状态检测来源
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<StateSource>))]
public enum StateSource
{
    /// <summary>ANSI OSC 结构化标记（优先级高）</summary>
    [JsonStringEnumMemberName("osc_marker")]
    OscMarker,

    /// <summary>正则解析回退</summary>
    [JsonStringEnumMemberName("regex_fallback")]
    RegexFallback
}
