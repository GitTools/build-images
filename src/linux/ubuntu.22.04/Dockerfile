FROM ubuntu:22.04

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y wget apt-transport-https \
    && wget -q https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb

RUN apt-get update \
    && apt-get install -y git \
    ca-certificates libc6 libgcc-s1 libicu70 liblttng-ust1 libssl3 libstdc++6 libunwind8 zlib1g \
    && rm -rf /var/lib/apt/lists/*
