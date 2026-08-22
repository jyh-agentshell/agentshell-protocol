using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// Agent 运行状态
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AgentState>))]
public enum AgentState
{
    /// <summary>正在执行任务</summary>
    [JsonStringEnumMemberName("running")]
    Running,

    /// <summary>等待用户审批</summary>
    [JsonStringEnumMemberName("awaiting_approval")]
    AwaitingApproval,

    /// <summary>空闲等待输入</summary>
    [JsonStringEnumMemberName("idle")]
    Idle,

    /// <summary>遇到错误</summary>
    [JsonStringEnumMemberName("error")]
    Error,

    /// <summary>已终止</summary>
    [JsonStringEnumMemberName("terminated")]
    Terminated
}
