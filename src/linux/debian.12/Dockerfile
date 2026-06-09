FROM debian:12@sha256:ed4fcc40bb1162b6d2d32e7bec15044d13963779abbe63f67f1cd62b06220519

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y --no-install-recommends ca-certificates git libc6 \
    libgcc-s1 libicu72 libssl3 libstdc++6 wget zlib1g \
    && rm -rf /var/lib/apt/lists/*
