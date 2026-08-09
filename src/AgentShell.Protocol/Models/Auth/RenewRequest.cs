using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// daemon 续期请求。由 daemon 发送到 POST /auth/renew。
/// 注意：Bearer Token 通过 Authorization 头传递，signature 和 timestamp 通过自定义 HTTP 头传递，
/// 因此此 DTO body 为空；保留以备后续扩展。
/// </summary>
public sealed class RenewRequest
{
    // 当前版本无 body 字段；认证信息通过 HTTP 头和 Authorization 传递
}
