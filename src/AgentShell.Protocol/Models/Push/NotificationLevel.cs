using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// 通知事件分级。
/// 服务端根据 AgentState 和 AgentType 决定推送级别。
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NotificationLevel
{
    /// <summary>需要用户立即处理（如 awaiting_approval）</summary>
    [JsonStringEnumMemberName("critical")]
    Critical,

    /// <summary>执行失败或异常（如 error）</summary>
    [JsonStringEnumMemberName("warning")]
    Warning,

    /// <summary>一般状态变更（如 terminated）</summary>
    [JsonStringEnumMemberName("info")]
    Info
}
