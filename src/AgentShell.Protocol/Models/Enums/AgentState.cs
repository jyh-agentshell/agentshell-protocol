namespace AgentShell.Protocol.Models;

/// <summary>
/// Agent 运行状态
/// </summary>
public enum AgentState
{
    /// <summary>正在执行任务</summary>
    Running,

    /// <summary>等待用户审批</summary>
    AwaitingApproval,

    /// <summary>空闲等待输入</summary>
    Idle,

    /// <summary>遇到错误</summary>
    Error,

    /// <summary>已终止</summary>
    Terminated
}
