FROM ubuntu:22.04
LABEL maintainers="GitTools Maintainers"

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y wget apt-transport-https \
    && wget -q https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb

RUN apt-get update \
    && apt-get install -y git \
    libc6 libgcc1 libgssapi-krb5-2 libicu66 libssl1.1 libstdc++6 zlib1g \
    && rm -rf /var/lib/apt/lists/*
