using System.Text.Json;
using Json.Schema;
using Xunit;

namespace AgentShell.Protocol.Tests;

public sealed class PushPayloadSchemaTests
{
    [Fact]
    public void P23推送Fixture符合严格Schema且拒绝敏感字段()
    {
        var schema = JsonSchema.FromText(File.ReadAllText(仓库文件("schemas", "push-payload.json")));
        using var valid = JsonDocument.Parse(File.ReadAllText(仓库文件("tests", "AgentShell.Protocol.Tests", "fixtures", "push-awaiting-approval.json")));
        using var invalid = JsonDocument.Parse("""
            {"protocol_version":"0.3.1","notification_id":"11111111-1111-4111-8111-111111111111","event_level":"critical","host_id":"22222222-2222-4222-8222-222222222222","session_id":"22222222-2222-4222-8222-222222222222/tmux/main","state":"awaiting_approval","occurred_at":"2026-08-11T00:00:00Z","expires_at":"2026-08-11T00:05:00Z","collapse_key":"host:22222222-2222-4222-8222-222222222222:session:main:state:awaiting_approval","deep_link":{"host_id":"22222222-2222-4222-8222-222222222222","session_id":"main"},"terminal_output":"secret"}
            """);

        Assert.True(schema.Evaluate(valid.RootElement).IsValid);
        Assert.False(schema.Evaluate(invalid.RootElement).IsValid);
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
        throw new FileNotFoundException(string.Join('/', segments));
    }
}
