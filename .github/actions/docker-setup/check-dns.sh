#!/usr/bin/env bash
set -euo pipefail

# Exercise the same BuildKit networking as the image builds on each native runner.
# Disable the layer cache so every job actually checks DNS.
docker buildx build --no-cache --progress=plain - <<'DOCKERFILE'
FROM ubuntu:24.04
RUN set -eu; \
    echo 'Build container DNS configuration:'; \
    cat /etc/resolv.conf; \
    for host in ports.ubuntu.com archive.ubuntu.com security.ubuntu.com \
                dot.net aka.ms builds.dotnet.microsoft.com; do \
        echo "Resolving ${host}"; \
        getent ahostsv4 "${host}" || { \
            echo "DNS resolution failed for ${host}" >&2; \
            exit 1; \
        }; \
    done
DOCKERFILE
