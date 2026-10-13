# build-images

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=GitTools_build-images&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=GitTools_build-images) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=GitTools_build-images&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=GitTools_build-images) [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=GitTools_build-images&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=GitTools_build-images) [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=GitTools_build-images&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=GitTools_build-images)

Docker build images for GitVersion & LibGit2Sharp

## Container user policy

All six Linux dependency images intentionally retain their base image's root user.
This policy also applies to the SDK/runtime images built from them. The generated
Dockerfiles install .NET into `/usr/local/bin`; GitVersion's downstream Dockerfile
installs its tool into `/tools` and configures Git in the image user's home directory.
Changing the inherited user requires coordinated changes and validation in GitVersion.

The previous non-root `gittools` user was reverted in commit
`1c848852f342223a523659a7451f762988a3f43b` after downstream compatibility failures.
Do not reintroduce it solely to clear a static-analysis finding.

For Sonar rule `docker:S6471`, use the same reviewed compatibility exception for
Alpine 3.23, CentOS Stream 10, Debian 13, Fedora 44, Ubuntu 24.04, and Ubuntu 26.04.
These are genuine root-user findings, not false positives. Retain the accepted
issues and document the rationale when reviewing the security hotspots; do not
disable the rule or exclude the Dockerfiles from analysis.

Root in a container remains a security tradeoff, especially with writable host
mounts. Run these images in isolated build environments without privileged mode
or a mounted Docker socket. Consumers choosing a non-root runtime user must
provide suitable home, temporary-directory, and repository-volume permissions
and validate GitVersion with that configuration. A future default-user change
must pass GitVersion's Docker build and test matrix across supported distros,
architectures, .NET versions, and Git backends before publication.
