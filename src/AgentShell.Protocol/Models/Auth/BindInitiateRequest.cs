using System.Text.Json.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>
/// App 发起绑定请求。携带从 daemon QR 码扫描得到的 6 位绑定码。
/// </summary>
public sealed class BindInitiateRequest
{
    /// <summary>6 位绑定码</summary>
    [JsonPropertyName("binding_code")]
    [JsonRequired]
    public required string BindingCode { get; init; }
}
