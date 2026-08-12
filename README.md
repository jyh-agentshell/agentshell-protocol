# AgentShell Protocol

[![Apache-2.0](https://img.shields.io/badge/license-Apache--2.0-blue)](./LICENSE)
[![NuGet](https://img.shields.io/nuget/v/AgentShell.Protocol.svg)](https://www.nuget.org/packages/AgentShell.Protocol)

**AgentShell 通信协议** — AI 编码代理移动端遥控器的开放接口契约。

本仓库是 AgentShell 项目的协议定义层，包含两层结构：

- **`schemas/`** — 语言无关的 JSON Schema，供 Kotlin 端和第三方实现者使用
- **`src/AgentShell.Protocol/`** — C# 强类型模型 NuGet 包，daemon 和 server 共同引用

## 仓库角色

在 AgentShell 架构中，本仓库定义的协议是 daemon、server、Android client 三者之间唯一的合法通信语言。

```
Android Client ←──HTTPS/WebSocket──→ .NET Server ←──HTTPS JSON──→ Daemon
     (JSON Schema)                       (NuGet)                     (NuGet)
```

## 目录结构

```
agentshell-protocol/
├── schemas/                    ← JSON Schema（语言无关）
│   ├── events/                 ← Agent 状态事件
│   │   ├── agent-state.json
│   │   └── session-lifecycle.json
│   ├── push-payload.json       ← 推送载荷
│   ├── ws-message.json         ← WebSocket 消息
│   └── host-sync.json          ← 主机同步 API
├── specs/                      ← 协议规范说明
│   ├── ansi-osc-markers.md     ← ANSI OSC 结构化标记规范
│   └── api-v1.yaml             ← OpenAPI 3.1 REST API 契约
├── src/AgentShell.Protocol/    ← C# 类库（NuGet 包）
│   └── ...
├── .github/workflows/          ← CI/CD（NuGet 发布）
└── LICENSE                     ← Apache-2.0
```

## 版本策略

- **NuGet 包版本号 ≡ Schema 版本号**，当前 P2.1 为 `0.3.0`
- 通信协议向后兼容至少 2 个大版本
- `schemas/` 中的 JSON Schema 是权威定义，C# 模型是对 Schema 的强类型实现

## 设计原则

1. **Agent 无关** — 协议抽象不绑定任何特定 CLI 工具
2. **隐私优先** — 服务端只知状态（awaiting_approval），不知代码内容和 SSH 密钥
3. **终端数据不经服务端** — SSH 直连，本协议只传信令 JSON

## 许可

Apache-2.0 — 公开接口契约，鼓励第三方兼容实现。
