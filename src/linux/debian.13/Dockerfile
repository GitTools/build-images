FROM debian:13

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y --no-install-recommends ca-certificates git libc6 \
    libgcc-s1 libgssapi-krb5-2 libicu76 libssl3t64 libstdc++6 tzdata wget zlib1g \
    && rm -rf /var/lib/apt/lists/*
