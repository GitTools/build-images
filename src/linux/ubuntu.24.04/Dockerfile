FROM ubuntu:24.04@sha256:786a8b558f7be160c6c8c4a54f9a57274f3b4fb1491cf65146521ae77ff1dc54

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y --no-install-recommends ca-certificates git \
    libc6 libgcc-s1 libicu74 libssl3t64 libstdc++6 wget zlib1g \
    && rm -rf /var/lib/apt/lists/*
