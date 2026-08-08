using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// Agent 运行状态
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AgentState
{
    /// <summary>正在执行任务</summary>
    [JsonPropertyName("running")]
    Running,

    /// <summary>等待用户审批</summary>
    [JsonPropertyName("awaiting_approval")]
    AwaitingApproval,

    /// <summary>空闲等待输入</summary>
    [JsonPropertyName("idle")]
    Idle,

    /// <summary>遇到错误</summary>
    [JsonPropertyName("error")]
    Error,

    /// <summary>已终止</summary>
    [JsonPropertyName("terminated")]
    Terminated
}
