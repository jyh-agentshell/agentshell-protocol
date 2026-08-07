# ANSI OSC 结构化标记规范

> **版本**: v1.0
> **状态**: Proposed Standard

## 概述

AgentShell 定义一套 ANSI OSC（Operating System Command）转义序列，作为 Agent CLI 工具与守护进程之间的**结构化通信通道**。

当 Agent CLI 工具检测到环境变量 `AGENTSHELL_SESSION_ID` 存在时，应在状态变化时主动发送对应的 OSC 序列。

## 环境变量

| 变量名 | 说明 |
|--------|------|
| `AGENTSHELL_SESSION_ID` | 当前 AgentShell 会话标识 |
| `AGENTSHELL_TOKEN` | 会话级一次性令牌，用于向 mDNS 本地 API 验证审批操作来源 |

## OSC 序列格式

```
ESC ] 9 ; agent_state=<state>[; <key>=<value>]* BEL
```

- `ESC` = 0x1B
- `BEL` = 0x07
- 键值对以 `; ` 分隔，键值内部以 `=` 分隔
- 多条序列之间互不干扰（OSC 标准保证终端正确处理未知 OSC 序列）

## 支持的状态

### `agent_state=running`

Agent 正在执行任务中。

```
ESC ] 9 ; agent_state=running BEL
```

参数: 无

### `agent_state=awaiting_approval`

Agent 等待用户审批。

```
ESC ] 9 ; agent_state=awaiting_approval; files=3; prompt=Approve changes? (y/n/d/r) BEL
```

参数:

| 键 | 类型 | 必需 | 说明 |
|----|------|------|------|
| `files` | int | 是 | 涉及的文件数量 |
| `prompt` | string | 是 | Agent 展示给用户的审批提示文本。base64 编码。 |

### `agent_state=idle`

Agent 空闲，等待用户输入。

```
ESC ] 9 ; agent_state=idle BEL
```

参数: 无

### `agent_state=error`

Agent 遇到错误。

```
ESC ] 9 ; agent_state=error; message=QnVpbGQgZmFpbGVkIGF0IGxpbmUgNDI= BEL
```

参数:

| 键 | 类型 | 必需 | 说明 |
|----|------|------|------|
| `message` | string | 是 | 错误描述。base64 编码以避免特殊字符问题。 |

## 守护进程处理逻辑

1. 持续读取终端输出流
2. 检测到 `ESC ] 9 ;` 前缀 → 进入 OSC 解析
3. 遇到 `BEL` (0x07) → 解析结束，提取状态
4. 未知键静默忽略（向前兼容）
5. 解析失败 → 降级到正则回退路径

## 与 CLI 工具集成建议

### Claude Code

```bash
export AGENTSHELL_SESSION_ID="..."
# Claude Code 在内部状态机切换时 emit OSC 序列
```

### Codex

```bash
# 类似机制，通过环境变量感知 AgentShell 存在
```

## 版本兼容性

- 守护进程遇到未知状态字符串 → 视为 `unknown`，视为 `running` 处理（不阻断）
- 守护进程遇到未知参数键 → 静默忽略
- CLI 工具不应假设守护进程解析成功，仍应在 stdout 输出常规审批文本（供正则回退路径）
