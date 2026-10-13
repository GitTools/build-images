FROM debian:13
# Root is intentional for downstream GitVersion image builds; see README.md#container-user-policy.

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y --no-install-recommends ca-certificates git libc6 \
    libgcc-s1 libgssapi-krb5-2 libicu76 libssl3t64 libstdc++6 tzdata wget zlib1g \
    && rm -rf /var/lib/apt/lists/*
