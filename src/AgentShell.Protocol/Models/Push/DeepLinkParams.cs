using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>受验证的只读会话摘要深链参数；客户端必须重新拉取状态后才允许 SSH 接管。</summary>
public sealed record DeepLinkParams(
    [property: JsonPropertyName("host_id")][property: JsonRequired] string HostId,
    [property: JsonPropertyName("session_id")][property: JsonRequired] string SessionId);
