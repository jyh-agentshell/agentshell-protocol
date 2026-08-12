using System.Security.Cryptography;
using System.Text.Json;
using AgentShell.Protocol.Models;
using Xunit;

namespace AgentShell.Protocol.Tests;

public sealed class ReportEnvelopeSerializationTests
{
    [Fact]
    public void P21协议包与Envelope使用同一版本() =>
        Assert.Equal("0.3.0", typeof(ReportEnvelope).Assembly.GetName().Version!.ToString(3));

    [Fact]
    public void ReportEnvelope使用固定snake_case和UTC毫秒时间戳()
    {
        var envelope = new ReportEnvelope(
            "0.3.0",
            "11111111-1111-4111-8111-111111111111",
            DateTimeOffset.Parse("2026-08-11T00:00:00Z"),
            Convert.ToBase64String(new byte[16]),
            ["agent_state"],
            "agent_state",
            Convert.ToBase64String("{}"u8.ToArray()),
            Convert.ToHexStringLower(SHA256.HashData("{}"u8)),
            Convert.ToBase64String(new byte[64]));

        var json = JsonSerializer.Serialize(envelope);

        Assert.Contains("\"protocol_version\":\"0.3.0\"", json, StringComparison.Ordinal);
        Assert.Contains("\"host_id\"", json, StringComparison.Ordinal);
        Assert.Contains("\"payload_base64\"", json, StringComparison.Ordinal);
        Assert.Contains("\"timestamp\":\"2026-08-11T00:00:00.000Z\"", json, StringComparison.Ordinal);
        Assert.DoesNotContain("ProtocolVersion", json, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("2026-08-11T00:00:00.000Z", true)]
    [InlineData("2026-08-11T00:00:00Z", false)]
    [InlineData("2026-08-11T08:00:00.000+08:00", false)]
    public void ProtocolTimestamp只接受固定UTC毫秒格式(string value, bool expected)
    {
        Assert.Equal(expected, ProtocolTimestamp.TryParse(value, out _));
    }
}
