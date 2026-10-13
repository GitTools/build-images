FROM ubuntu:26.04

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y --no-install-recommends ca-certificates git \
    libc6 libgcc-s1 libgssapi-krb5-2 libicu78 libssl3t64 libstdc++6 tzdata wget zlib1g \
    && rm -rf /var/lib/apt/lists/* \
    && groupadd --gid 10001 gittools \
    && useradd --uid 10001 --gid 10001 --create-home --shell /bin/bash gittools \
    && mkdir -p /home/gittools/app \
    && chown -R 10001:10001 /home/gittools

ENV HOME=/home/gittools
WORKDIR /home/gittools/app
USER 10001:10001
