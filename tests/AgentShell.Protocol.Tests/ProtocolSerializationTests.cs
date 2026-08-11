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

    [Fact]
    public void HostSync_模型和Schema不定义私钥语义()
    {
        var hostConfigSource = File.ReadAllText(获取仓库文件路径("src", "AgentShell.Protocol", "Models", "HostSync", "HostConfig.cs"));
        var hostSyncSchema = File.ReadAllText(获取仓库文件路径("schemas", "host-sync.json"));

        Assert.DoesNotContain("PrivateKey", hostConfigSource, StringComparison.Ordinal);
        Assert.DoesNotContain("private_key", hostConfigSource, StringComparison.Ordinal);
        Assert.DoesNotContain("private_key", hostSyncSchema, StringComparison.Ordinal);
    }

    [Fact]
    public void RenewResponse_序列化与反序列化_RoundTrip()
    {
        var original = new RenewResponse
        {
            AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJob3N0XzEifQ.signature",
            TokenType = "Bearer",
            ExpiresIn = 3600
        };
        var json = JsonSerializer.Serialize(original);
        var deserialized = JsonSerializer.Deserialize<RenewResponse>(json);
        Assert.NotNull(deserialized);
        Assert.Equal(original.AccessToken, deserialized!.AccessToken);
        Assert.Equal(3600, deserialized.ExpiresIn);
    }

    [Fact]
    public void BindInitiateResponse_序列化使用snake_case()
    {
        var response = new BindInitiateResponse
        {
            ChallengeId = Guid.NewGuid().ToString(),
            Nonce = Convert.ToBase64String(new byte[32]),
            TtlSeconds = 300
        };
        var json = JsonSerializer.Serialize(response);
        Assert.Contains("challenge_id", json, StringComparison.Ordinal);
        Assert.Contains("ttl_seconds", json, StringComparison.Ordinal);
    }

    [Fact]
    public void BindConfirmRequest_序列化使用snake_case()
    {
        var request = new BindConfirmRequest
        {
            ChallengeId = "challenge-123",
            BindingCode = "123456",
            HostId = "5a649887-2922-42ac-97d1-2ee5a9ec6335",
            Signature = "base64signature...",
            PublicKey = "base64publickey..."
        };
        var json = JsonSerializer.Serialize(request);
        Assert.Contains("challenge_id", json, StringComparison.Ordinal);
        Assert.Contains("binding_code", json, StringComparison.Ordinal);
        Assert.Contains("host_id", json, StringComparison.Ordinal);
        Assert.Contains("signature", json, StringComparison.Ordinal);
        Assert.Contains("public_key", json, StringComparison.Ordinal);
    }

    [Fact]
    public void BindConfirmResponse_序列化与反序列化_RoundTrip()
    {
        var original = new BindConfirmResponse
        {
            Bound = true,
            HostId = Guid.NewGuid().ToString("D"),
            AccessToken = "eyJhbGciOiJIUzI1NiJ9.test",
            TokenType = "Bearer",
            ExpiresIn = 3600
        };
        var json = JsonSerializer.Serialize(original);
        var deserialized = JsonSerializer.Deserialize<BindConfirmResponse>(json);
        Assert.NotNull(deserialized);
        Assert.True(deserialized!.Bound);
        Assert.Equal(original.HostId, deserialized.HostId);
        Assert.Equal(original.AccessToken, deserialized.AccessToken);
    }

    // ─── P2.1/2.2 新增测试 ────────────────────────────────

    [Fact]
    public void AgentStateEvent_序列化与反序列化_RoundTrip()
    {
        var original = new AgentStateEvent
        {
            EventId = "evt_001",
            Timestamp = DateTimeOffset.Parse("2026-08-10T00:00:00+00:00"),
            SessionId = "host-123/tmux/session_alpha",
            AgentType = AgentType.Claude,
            State = AgentState.AwaitingApproval,
            PreviousState = AgentState.Running,
            Detail = new AgentStateDetail { FileCount = 3 },
            Source = StateSource.OscMarker,
            ProtocolVersion = "0.2.0",
            DaemonVersion = "0.2.0"
        };
        var json = JsonSerializer.Serialize(original);
        var deserialized = JsonSerializer.Deserialize<AgentStateEvent>(json);
        Assert.NotNull(deserialized);
        Assert.Equal(original.EventId, deserialized!.EventId);
        Assert.Equal(original.SessionId, deserialized.SessionId);
        Assert.Equal(AgentState.AwaitingApproval, deserialized.State);
        Assert.Equal(AgentType.Claude, deserialized.AgentType);
        Assert.Equal(StateSource.OscMarker, deserialized.Source);
        Assert.NotNull(deserialized.Detail);
        Assert.Equal(3, deserialized.Detail!.FileCount);
        // 验证 snake_case 序列化
        Assert.Contains("event_id", json, StringComparison.Ordinal);
        Assert.Contains("session_id", json, StringComparison.Ordinal);
        Assert.Contains("agent_type", json, StringComparison.Ordinal);
        Assert.Contains("previous_state", json, StringComparison.Ordinal);
        Assert.Contains("protocol_version", json, StringComparison.Ordinal);
    }

    [Fact]
    public void SessionLifecycleEvent_序列化与反序列化_RoundTrip()
    {
        var original = new SessionLifecycleEvent
        {
            EventId = "evt_002",
            Timestamp = DateTimeOffset.Parse("2026-08-10T00:01:00+00:00"),
            SessionId = "host-123/tmux/session_beta",
            EventType = SessionEventType.Created,
            MultiplexerType = MultiplexerType.Tmux,
            SessionName = "session_beta",
            AgentType = AgentType.Codex,
            PaneCount = 2,
            ProtocolVersion = "0.2.0"
        };
        var json = JsonSerializer.Serialize(original);
        var deserialized = JsonSerializer.Deserialize<SessionLifecycleEvent>(json);
        Assert.NotNull(deserialized);
        Assert.Equal(original.EventId, deserialized!.EventId);
        Assert.Equal(original.SessionId, deserialized.SessionId);
        Assert.Equal(SessionEventType.Created, deserialized.EventType);
        Assert.Equal(MultiplexerType.Tmux, deserialized.MultiplexerType);
        Assert.Equal(2, deserialized.PaneCount);
        // 验证 snake_case 枚举序列化
        Assert.Contains("session_created", json, StringComparison.Ordinal);
        Assert.Contains("tmux", json, StringComparison.Ordinal);
        Assert.Contains("event_type", json, StringComparison.Ordinal);
        Assert.Contains("multiplexer_type", json, StringComparison.Ordinal);
    }

    [Fact]
    public void WsMessage_序列化与反序列化_RoundTrip()
    {
        using var payloadDoc = JsonDocument.Parse("""{"session_id":"s1","state":"running"}""");
        var original = new WsMessage
        {
            MessageId = "msg_001",
            Timestamp = DateTimeOffset.Parse("2026-08-10T00:02:00+00:00"),
            Type = WsMessageType.AgentStateChanged,
            Direction = MessageDirection.ServerToClient,
            Payload = payloadDoc.RootElement.Clone(),
            CorrelationId = "corr_001"
        };
        var json = JsonSerializer.Serialize(original);
        var deserialized = JsonSerializer.Deserialize<WsMessage>(json);
        Assert.NotNull(deserialized);
        Assert.Equal(original.MessageId, deserialized!.MessageId);
        Assert.Equal(WsMessageType.AgentStateChanged, deserialized.Type);
        Assert.Equal(MessageDirection.ServerToClient, deserialized.Direction);
        Assert.Equal("corr_001", deserialized.CorrelationId);
        // Payload 是任意 JSON object
        Assert.Equal(JsonValueKind.Object, deserialized.Payload.ValueKind);
        // 验证 snake_case 枚举
        Assert.Contains("agent_state_changed", json, StringComparison.Ordinal);
        Assert.Contains("server_to_client", json, StringComparison.Ordinal);
    }

    [Fact]
    public void PushPayload_序列化与反序列化_RoundTrip()
    {
        var original = new PushPayload
        {
            NotificationId = "notif_001",
            Timestamp = DateTimeOffset.Parse("2026-08-10T00:03:00+00:00"),
            Type = PushType.ApprovalRequired,
            Title = "测试主机 需要审批",
            Body = "Claude Code 有 3 个文件变更需要审批",
            Data = new PushData
            {
                SessionId = "host-123/tmux/session_alpha",
                HostId = "host-123",
                AgentType = "claude",
                State = "awaiting_approval",
                FileCount = 3,
                ActionUrl = "agentshell://host/host-123/session/host-123%2Ftmux%2Fsession_alpha?action=terminal_takeover"
            },
            Priority = PushPriority.High,
            TtlSeconds = 300
        };
        var json = JsonSerializer.Serialize(original);
        var deserialized = JsonSerializer.Deserialize<PushPayload>(json);
        Assert.NotNull(deserialized);
        Assert.Equal(original.NotificationId, deserialized!.NotificationId);
        Assert.Equal(PushType.ApprovalRequired, deserialized.Type);
        Assert.Equal(PushPriority.High, deserialized.Priority);
        Assert.NotNull(deserialized.Data);
        Assert.Equal(3, deserialized.Data!.FileCount);
        Assert.Contains("action_url", json, StringComparison.Ordinal);
        // 验证数据载荷字段隐私边界：不包含代码/终端内容
        Assert.DoesNotContain("source_code", json, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("terminal_output", json, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ssh_key", json, StringComparison.OrdinalIgnoreCase);
        // 验证 snake_case
        Assert.Contains("notification_id", json, StringComparison.Ordinal);
        Assert.Contains("approval_required", json, StringComparison.Ordinal);
    }

    [Fact]
    public void RenewRequest_序列化为空Body()
    {
        var request = new RenewRequest();
        var json = JsonSerializer.Serialize(request);
        Assert.Equal("{}", json);
    }

    [Fact]
    public void BindInitiateRequest_序列化与反序列化_RoundTrip()
    {
        var original = new BindInitiateRequest
        {
            BindingCode = "123456"
        };
        var json = JsonSerializer.Serialize(original);
        Assert.Contains("binding_code", json, StringComparison.Ordinal);
        Assert.Contains("123456", json, StringComparison.Ordinal);

        var deserialized = JsonSerializer.Deserialize<BindInitiateRequest>(json);
        Assert.NotNull(deserialized);
        Assert.Equal("123456", deserialized!.BindingCode);
    }

    private static string 获取仓库文件路径(params string[] pathSegments)
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var candidate = Path.Combine([directory.FullName, .. pathSegments]);
            if (File.Exists(candidate))
            {
                return candidate;
            }

            directory = directory.Parent;
        }

        throw new FileNotFoundException($"未找到仓库文件：{Path.Combine(pathSegments)}");
    }
}
