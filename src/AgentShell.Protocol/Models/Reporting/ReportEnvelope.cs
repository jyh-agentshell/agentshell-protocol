using System.Text.Json.Serialization;
using AgentShell.Protocol.Serialization;

namespace AgentShell.Protocol.Models;

/// <summary>daemon 上报状态前签名的不可变信封。</summary>
public sealed record ReportEnvelope(
    [property: JsonPropertyName("protocol_version")] string ProtocolVersion,
    [property: JsonPropertyName("host_id")] string HostId,
    [property: JsonPropertyName("timestamp")]
    [property: JsonConverter(typeof(ProtocolTimestampJsonConverter))] DateTimeOffset Timestamp,
    [property: JsonPropertyName("nonce")] string Nonce,
    [property: JsonPropertyName("capabilities")] IReadOnlyList<string> Capabilities,
    [property: JsonPropertyName("payload_type")] string PayloadType,
    [property: JsonPropertyName("payload_base64")] string PayloadBase64,
    [property: JsonPropertyName("payload_sha256")] string PayloadSha256,
    [property: JsonPropertyName("signature")] string Signature);
