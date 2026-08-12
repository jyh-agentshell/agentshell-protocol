using System.Reflection;
using SharpYaml.Serialization;
using Xunit;

namespace AgentShell.Protocol.Tests;

public sealed class P21OpenApiContractTests
{
    [Fact]
    public void P21状态路由声明签名Envelope与快照兼容窗口()
    {
        var specification = File.ReadAllText(规范路径());
        var source = new YamlStream();
        using var input = new StringReader(specification);
        source.Load(input);
        var root = (YamlMappingNode)source.Documents[0].RootNode;
        var paths = 映射(root, "paths");

        Assert.True(包含路径(paths, "/auth/device"));
        Assert.True(包含路径(paths, "/auth/renew"));
        Assert.True(包含路径(paths, "/devices/bind"));
        Assert.True(包含路径(paths, "/devices/bind/verify"));
        Assert.True(包含路径(paths, "/sessions/report"));
        Assert.True(包含路径(paths, "/sessions/lifecycle"));
        Assert.True(包含路径(paths, "/sessions"));
        Assert.False(包含路径(paths, "/sessions/{session_id}/status"));
        Assert.Contains("ReportEnvelope", specification, StringComparison.Ordinal);
        Assert.Contains("protocol_min", specification, StringComparison.Ordinal);
        Assert.Contains("protocol_max", specification, StringComparison.Ordinal);
        Assert.Contains("feature_enabled", specification, StringComparison.Ordinal);
    }

    private static bool 包含路径(YamlMappingNode paths, string expected) =>
        paths.Children.Keys.OfType<YamlScalarNode>().Any(key => key.Value == expected);

    private static YamlMappingNode 映射(YamlMappingNode root, string key) =>
        (YamlMappingNode)root.Children.Single(pair => ((YamlScalarNode)pair.Key).Value == key).Value;

    private static string 规范路径() => Assembly.GetExecutingAssembly()
        .GetCustomAttributes<AssemblyMetadataAttribute>()
        .Single(attribute => attribute.Key == "OpenApiSpecificationPath")
        .Value!;
}
