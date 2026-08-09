using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// Agent 状态的详细信息
/// </summary>
public sealed class AgentStateDetail
{
    /// <summary>审批涉及的文件数量</summary>
    [JsonPropertyName("file_count")]
    [JsonPropertyOrder(1)]
    public int? FileCount { get; init; }

    /// <summary>受限错误码，不包含终端输出。</summary>
    [JsonPropertyName("error_code")]
    [JsonPropertyOrder(2)]
    public string? ErrorCode { get; init; }
}
