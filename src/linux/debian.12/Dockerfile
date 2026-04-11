FROM debian:12

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y --no-install-recommends ca-certificates git libc6 \
    libgcc-s1 libicu72 libssl3 libstdc++6 wget zlib1g \
    && rm -rf /var/lib/apt/lists/* \
    && chmod 1777 /tmp \
    && groupadd --gid 10001 gittools \
    && useradd --uid 10001 --gid 10001 --create-home --shell /bin/bash gittools

USER gittools
