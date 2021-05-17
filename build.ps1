#!/usr/bin/env pwsh
if (!(Test-Path run)) {
    dotnet build build/CI.sln
}
dotnet run/build.dll $args
exit $LASTEXITCODE;
