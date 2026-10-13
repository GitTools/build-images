# build-images

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=GitTools_build-images&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=GitTools_build-images) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=GitTools_build-images&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=GitTools_build-images) [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=GitTools_build-images&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=GitTools_build-images) [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=GitTools_build-images&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=GitTools_build-images)

Docker build images for GitVersion & LibGit2Sharp

## Container user policy

All six Linux dependency images and their SDK/runtime derivatives default to
the `gittools` user, UID/GID `10001:10001`, with home `/home/gittools`.
Root is used only during image assembly to install OS packages and .NET into
`/usr/local/bin`. Generated Dockerfiles restore `USER 10001:10001` after installation.
The build checks the default UID before publishing each image.

Downstream Dockerfiles that install into system-owned paths must explicitly
switch to root for those build steps and restore a non-root final user.
GitVersion's corresponding changes are tracked in
[GitTools/GitVersion#5207](https://github.com/GitTools/GitVersion/pull/5207).
Validate that consumer before merging this change: an earlier non-root setup
was reverted in `1c848852f342223a523659a7451f762988a3f43b` after downstream failures.

Bind-mounted repositories must be writable by the container user when tools
update Git state or generate build outputs. On Linux, run as the non-root checkout
owner with `--user "$(id -u):$(id -g)"`; if that UID differs from 10001, also supply
a writable container-local home, for example `--env HOME=/tmp --env DOTNET_CLI_HOME=/tmp`.
Do not make the checkout world-writable or recursively change its ownership.

The GitVersion Docker and artifact test matrix must pass across both architectures,
supported distros, .NET versions, and Git backends before rollout. Sonar's previously
accepted root-user findings must be re-evaluated after analysis of these changes;
the earlier exception does not count as verification of the non-root implementation.
