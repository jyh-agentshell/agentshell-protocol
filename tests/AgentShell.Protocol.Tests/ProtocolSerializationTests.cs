using System.Text.Json;
using System.Runtime.CompilerServices;
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

    [Fact]
    public void HostConfig_序列化仅包含加密主机同步字段()
    {
        var config = new HostConfig
        {
            HostId = "5a649887-2922-42ac-97d1-2ee5a9ec6335",
            Ciphertext = "Q2lwaGVydGV4dA==",
            Nonce = "Tm9uY2U=",
            Aad = "SG9zdElk",
            EncryptionVersion = 1,
            UpdatedAt = DateTimeOffset.Parse("2026-08-09T00:00:00+00:00")
        };

        var json = JsonSerializer.Serialize(config);

        Assert.Contains("host_id", json, StringComparison.Ordinal);
        Assert.Contains("ciphertext", json, StringComparison.Ordinal);
        Assert.Contains("nonce", json, StringComparison.Ordinal);
        Assert.Contains("aad", json, StringComparison.Ordinal);
        Assert.Contains("encryption_version", json, StringComparison.Ordinal);
        Assert.Contains("updated_at", json, StringComparison.Ordinal);
        Assert.DoesNotContain("hostname", json, StringComparison.Ordinal);
        Assert.DoesNotContain("username", json, StringComparison.Ordinal);
        Assert.DoesNotContain("port", json, StringComparison.Ordinal);
        Assert.DoesNotContain("auth_type", json, StringComparison.Ordinal);

        using var document = JsonDocument.Parse(json);
        var fieldNames = document.RootElement.EnumerateObject()
            .Select(property => property.Name)
            .Order()
            .ToArray();

        Assert.Equal(
            ["aad", "ciphertext", "encryption_version", "host_id", "nonce", "updated_at"],
            fieldNames);
    }

    [Fact]
    public void HostConfig_加密版本和更新时间为必填字段()
    {
        var encryptionVersion = typeof(HostConfig).GetProperty(nameof(HostConfig.EncryptionVersion));
        var updatedAt = typeof(HostConfig).GetProperty(nameof(HostConfig.UpdatedAt));

        Assert.NotNull(encryptionVersion);
        Assert.NotNull(updatedAt);
        Assert.NotNull(encryptionVersion.GetCustomAttributes(typeof(RequiredMemberAttribute), inherit: false).SingleOrDefault());
        Assert.NotNull(updatedAt.GetCustomAttributes(typeof(RequiredMemberAttribute), inherit: false).SingleOrDefault());
    }
}
