# Copilot Instructions

## Overview

This repository builds and publishes two families of Docker images for GitTools (GitVersion / LibGit2Sharp):

- **`gittools/deps`** – per-distro base images (git, ICU libs, etc.). One image per distro + arch.
- **`gittools/build-images`** – build images with .NET SDK or runtime installed on top of deps. One image per distro + .NET version + variant (sdk/runtime) + arch.

## Build System

The build is driven by [Cake Frosting](https://cakebuild.net/docs/running-builds/runners/cake-frosting) (C# build orchestration), located in `build/build/`. Compiled output goes to `run/build.dll`.

### Commands

```powershell
# Compile the Cake build project (needed once, or when build/ changes)
dotnet build build/

# Run a specific target
dotnet run/build.dll --target=<TaskName> [args...]

# Or use the convenience wrapper
./build.ps1 --target=<TaskName> [args...]
```

### Available Targets

| Target                      | Description                                            |
| --------------------------- | ------------------------------------------------------ |
| `SetMatrix`                 | Outputs CI matrix values (distros, versions, variants) |
| `DockerBuildDeps`           | Builds per-distro dependency images                    |
| `DockerBuildImages`         | Builds .NET build images (on top of deps)              |
| `DockerBuildDepsManifest`   | Creates multi-arch manifest for deps images            |
| `DockerBuildImagesManifest` | Creates multi-arch manifest for build images           |

### Build Arguments

| Argument            | Values                                              | Default (omitted) |
| ------------------- | --------------------------------------------------- | ----------------- |
| `--arch`            | `amd64`, `arm64`                                    | both              |
| `--dotnet_version`  | `10.0`, `9.0`, `8.0`, `lts-latest`                  | all versions      |
| `--dotnet_variant`  | `sdk`, `runtime`                                    | both              |
| `--docker_distro`   | e.g. `ubuntu.24.04`, `alpine.3.23`, `distro-latest` | all distros       |
| `--docker_registry` | `dockerhub`, `github`                               | dockerhub         |
| `--push_images`     | `true`, `false`                                     | `false`           |

`distro-latest` resolves to `alpine.3.23`; `lts-latest` resolves to `10.0`.

### Example: build a single image locally

```powershell
dotnet run/build.dll --target=DockerBuildDeps --arch=amd64 --docker_distro=ubuntu.24.04
dotnet run/build.dll --target=DockerBuildImages --arch=amd64 --dotnet_version=10.0 --dotnet_variant=sdk --docker_distro=ubuntu.24.04
```

## Architecture

### Image Layers

```
gittools/deps:<distro>-<arch>              (src/linux/<distro>/Dockerfile)
    └── gittools/build-images:<distro>-<variant>-<version>-<arch>  (src/linux/Dockerfile + generated Dockerfile.build)
```

### Dockerfile Generation

`DockerBuildImages` dynamically generates `src/linux/Dockerfile.build` at build time by appending a `dotnet-install.sh` script to `src/linux/Dockerfile`. For the `runtime` variant, `--runtime dotnet` is added to the install command. The generated file is not committed.

### Supported Distros

`alpine.3.23`, `centos.stream.9`, `debian.12`, `fedora.43`, `ubuntu.22.04`, `ubuntu.24.04`

These are defined as constants in `build/build/Utils/Constants.cs` and reflect the actual directory names under `src/linux/`.

### .NET Versions and Variants

- Versions: `10.0` (LTS latest), `9.0`, `8.0`
- Variants: `sdk`, `runtime`

Both are defined in `Constants.cs`.

## CI Workflows

### Workflow DAG

```
push/PR to main ──► build-deps.yml
                         │
                         └──► (on completion, non-fork, non-PR) build-images.yml
```

Both top-level workflows (`build-deps.yml`, `build-images.yml`) use reusable workflows prefixed with `_`:

- `_build-deps.yml` / `_build-images.yml` – per-arch build jobs
- `_build-deps-manifest.yml` / `_build-images-manifest.yml` – multi-arch manifest creation

### Push Conditions

- **`build-deps.yml`**: images pushed only when `github.event_name != 'pull_request' && github.repository_owner == 'GitTools'`
- **`build-images.yml`**: images pushed only when `github.repository_owner == 'GitTools'`

Reusable workflows receive `push_images` as an explicit boolean input — they do not infer it from `github.event_name`.

### Composite Actions

Under `.github/actions/`, each action wraps a call to `dotnet run/build.dll` via PowerShell. The `PUSH_IMAGES` env var is passed as an environment variable and checked with `if: env.PUSH_IMAGES == 'true'` (string comparison, not boolean).

### Credentials

DockerHub credentials are retrieved at runtime from 1Password via `gittools/cicd/dockerhub-creds`, using `OP_SERVICE_ACCOUNT_TOKEN`. GHCR login uses `github.token`.

## Key Conventions

- **C# 14 / net10.0** – Cake Frosting project uses `LangVersion=14` including C# 14 extension members syntax (see `DockerExtensions.cs`).
- **`run/` directory** – compiled Cake output; cached in CI keyed on `hashFiles('./build/**')`. Rebuild it locally by deleting `run/` and running `dotnet build build/`.
- **OCI labels** – all images get `org.opencontainers.image.*` labels and annotations with standard metadata (authors, vendor, license, source, created).
- **Naming pattern for adding a new distro**: add a `Dockerfile` under `src/linux/<distro>/` following existing examples, and add the distro string to `Constants.DockerDistros` in `build/build/Utils/Constants.cs`.
