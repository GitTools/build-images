FROM ubuntu:20.04

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y wget apt-transport-https \
    && wget -q https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb

RUN apt-get update \
    && apt-get install -y git \
    ca-certificates libc6 libgcc-s1 libicu66 libssl1.1 libstdc++6 zlib1g \
    && rm -rf /var/lib/apt/lists/*
