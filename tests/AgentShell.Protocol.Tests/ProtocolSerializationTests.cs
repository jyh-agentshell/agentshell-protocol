using System.Text.Json;
using AgentShell.Protocol.Models;
using Xunit;

namespace AgentShell.Protocol.Tests;

public sealed class ProtocolSerializationTests
{
    [Fact]
    public void AgentStateDetail_序列化不包含终端原文()
    {
        var json = JsonSerializer.Serialize(new AgentStateDetail { FileCount = 3 });

        Assert.DoesNotContain("message", json, StringComparison.Ordinal);
        Assert.DoesNotContain("prompt", json, StringComparison.Ordinal);
        Assert.DoesNotContain("error_message", json, StringComparison.Ordinal);
    }
}
