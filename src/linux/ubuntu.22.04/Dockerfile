FROM ubuntu:22.04

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y --no-install-recommends ca-certificates git \
    libc6 libgcc-s1 libicu70 libssl3 libstdc++6 wget zlib1g \
    && rm -rf /var/lib/apt/lists/*
