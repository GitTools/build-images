#!/usr/bin/env pwsh
if (!(Test-Path run)) {
    dotnet build build/
}
dotnet run/build.dll $args
exit $LASTEXITCODE;
