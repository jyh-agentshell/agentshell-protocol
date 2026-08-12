#!/usr/bin/env bash
# 发布前统一验证 Schema、强类型模型、序列化与唯一 NuGet 产物。
set -euo pipefail

version_args=()
if [[ -n "${1:-}" ]]; then
  version_args=("-p:Version=$1")
fi

mkdir -p artifacts
find artifacts -maxdepth 1 -type f \( -name '*.nupkg' -o -name '*.snupkg' \) -delete
dotnet restore src/AgentShell.Protocol/AgentShell.Protocol.csproj
dotnet build src/AgentShell.Protocol/AgentShell.Protocol.csproj --configuration Release --no-restore "${version_args[@]}"
dotnet test tests/AgentShell.Protocol.Tests/AgentShell.Protocol.Tests.csproj --configuration Release --no-restore
dotnet pack src/AgentShell.Protocol/AgentShell.Protocol.csproj --configuration Release --no-build --output artifacts "${version_args[@]}"

count=$(find artifacts -maxdepth 1 -type f -name '*.nupkg' | wc -l)
test "$count" -eq 1
