FROM ubuntu:22.04

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y git wget \
    ca-certificates libc6 libgcc-s1 libicu70 libssl3 libstdc++6 zlib1g \
    && rm -rf /var/lib/apt/lists/*
