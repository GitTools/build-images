FROM debian:12

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y git libc6 \
    libgcc-s1 libicu72 libssl3 libstdc++6 wget zlib1g \
    && rm -rf /var/lib/apt/lists/*
