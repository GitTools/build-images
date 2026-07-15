FROM debian:12@sha256:30482e873082e906a4908c10529180aefb6f77620aea7404b909829fadc5d168

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y --no-install-recommends ca-certificates git libc6 \
    libgcc-s1 libicu72 libssl3 libstdc++6 wget zlib1g \
    && rm -rf /var/lib/apt/lists/*
