[CmdletBinding()]
Param(
    [string]$OS,
    [string]$Distro,
    [string]$Version,
    [string]$Tag
)

$dockerTag = "gittools/gitversion:$Tag-$OS-$Distro-netcoreapp$Version"

Write-Information "##[group]Docker Build"
docker build --build-arg "DOTNET_VERSION=$Version" --build-arg "DISTRO=$Distro" --build-arg "VERSION=$Tag" --file "recovery/Dockerfile.$OS" --rm=True --tag $dockerTag "recovery"
Write-Information "##[endgroup]"
Write-Information "##[group]Docker Build"
docker push $dockerTag
Write-Information "##[endgroup]"
