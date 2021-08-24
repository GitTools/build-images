FROM ubuntu:16.04
LABEL maintainers="GitTools Maintainers"

RUN apt-get update \
    && apt-get install -y wget apt-transport-https \
    && wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb

RUN apt-get update \
    && apt-get install -y git \
    libc6 libgcc1 libgssapi-krb5-2 libicu55 libssl1.0.0 libstdc++6 zlib1g -y \
    && rm -rf /var/lib/apt/lists/*
