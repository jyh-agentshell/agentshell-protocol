using System.Text.Json;
using Json.Schema;
using Xunit;

namespace AgentShell.Protocol.Tests;

public sealed class ReportEnvelopeSchemaTests
{
    private static readonly JsonSchema EnvelopeSchema =
        JsonSchema.FromText(File.ReadAllText(仓库文件("schemas", "report-envelope.json")));

    [Theory]
    [InlineData("""
        {"protocol_version":"0.3.0","host_id":"11111111-1111-4111-8111-111111111111","timestamp":"2026-08-11T00:00:00.000Z","nonce":"AAAAAAAAAAAAAAAAAAAAAA==","capabilities":["agent_state"],"payload_type":"agent_state","payload_base64":"e30=","payload_sha256":"44136fa355b3678a1146ad16f7e8649e94fb4fc21b6b0bcbf3c9b8f5e3f5d1d5","signature":"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=="}
        """, true)]
    [InlineData("""
        {"protocol_version":"0.3.0","host_id":"11111111-1111-4111-8111-111111111111","timestamp":"2026-08-11T00:00:00.000Z","capabilities":["agent_state"],"payload_type":"agent_state","payload_base64":"e30=","payload_sha256":"44136fa355b3678a1146ad16f7e8649e94fb4fc21b6b0bcbf3c9b8f5e3f5d1d5","signature":"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=="}
        """, false)]
    public void 上报Envelope严格校验(string json, bool expected)
    {
        using var document = JsonDocument.Parse(json);

        Assert.Equal(expected, EnvelopeSchema.Evaluate(document.RootElement).IsValid);
    }

    private static string 仓库文件(params string[] segments)
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var path = Path.Combine([directory.FullName, .. segments]);
            if (File.Exists(path)) return path;
            directory = directory.Parent;
        }

        throw new FileNotFoundException($"未找到仓库文件：{Path.Combine(segments)}");
    }
}
