FROM quay.io/centos/centos:stream9

# Pre-requisits
RUN dnf install -y git glibc-langpack-en \
    krb5-libs libicu openssl-libs wget zlib \
    && rm -rf /tmp/* \
    && chmod 1777 /tmp \
    && groupadd --gid 10001 gittools \
    && useradd --uid 10001 --gid 10001 --create-home --shell /bin/bash gittools

USER gittools
