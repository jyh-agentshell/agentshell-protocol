using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// Agent 状态的详细信息
/// </summary>
public sealed class AgentStateDetail
{
    /// <summary>人类可读的状态描述</summary>
    [JsonPropertyName("message")]
    [JsonPropertyOrder(1)]
    public string? Message { get; init; }

    /// <summary>Agent 的交互提示文本</summary>
    [JsonPropertyName("prompt")]
    [JsonPropertyOrder(2)]
    public string? Prompt { get; init; }

    /// <summary>审批涉及的文件数量</summary>
    [JsonPropertyName("file_count")]
    [JsonPropertyOrder(3)]
    public int? FileCount { get; init; }

    /// <summary>审批涉及的文件列表（仅文件名，不含完整路径）</summary>
    [JsonPropertyName("file_list")]
    [JsonPropertyOrder(4)]
    public string[]? FileList { get; init; }

    /// <summary>错误码</summary>
    [JsonPropertyName("error_code")]
    [JsonPropertyOrder(5)]
    public string? ErrorCode { get; init; }

    /// <summary>错误描述</summary>
    [JsonPropertyName("error_message")]
    [JsonPropertyOrder(6)]
    public string? ErrorMessage { get; init; }
}
