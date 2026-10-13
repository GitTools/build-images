FROM quay.io/centos/centos:stream10

# Pre-requisits
RUN dnf install -y ca-certificates git glibc glibc-langpack-en \
    krb5-libs libgcc libicu libstdc++ openssl-libs shadow-utils tzdata wget zlib \
    && rm -rf /tmp/* \
    && groupadd --gid 10001 gittools \
    && useradd --uid 10001 --gid 10001 --create-home --shell /bin/bash gittools \
    && mkdir -p /home/gittools/app \
    && chown -R 10001:10001 /home/gittools

ENV HOME=/home/gittools
WORKDIR /home/gittools/app
USER 10001:10001
