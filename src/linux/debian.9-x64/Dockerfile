FROM debian:9
LABEL maintainers="GitTools Maintainers"

RUN apt-get update \
    && apt-get install -y wget gpg apt-transport-https \
    && wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg \
    && mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/ \
    && wget -q https://packages.microsoft.com/config/debian/9/prod.list \
    && mv prod.list /etc/apt/sources.list.d/microsoft-prod.list

RUN apt-get update \
    && apt-get install -y git \
    libc6 libgcc1 libgssapi-krb5-2 libicu57 libssl1.1 libstdc++6 zlib1g \
    && rm -rf /var/lib/apt/lists/*
