using System.Text.Json;
using Xunit;

namespace AgentShell.Protocol.Tests;

public sealed class SchemaVersionConsistencyTests
{
    [Fact]
    public void 所有协议Schema使用冻结版本()
    {
        var schemaRoot = FindSchemaRoot();
        var schemas = Directory.EnumerateFiles(schemaRoot, "*.json", SearchOption.AllDirectories).ToList();
        Assert.NotEmpty(schemas);

        foreach (var path in schemas)
        {
            using var document = JsonDocument.Parse(File.ReadAllText(path));
            Assert.True(document.RootElement.TryGetProperty("x-version", out var version), path);
            Assert.Equal("0.3.1", version.GetString());
        }
    }

    private static string FindSchemaRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, "schemas");
            if (Directory.Exists(candidate)) return candidate;
            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("未找到协议 schemas 目录");
    }
}
