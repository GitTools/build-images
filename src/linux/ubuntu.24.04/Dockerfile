FROM ubuntu:24.04

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt install -y git \
    ca-certificates libc6 libgcc-s1 libicu74 liblttng-ust1 libssl3 libstdc++6 libunwind8 zlib1g \
    && rm -rf /var/lib/apt/lists/*

RUN apt-get update \
    && apt install -y wget apt-transport-https