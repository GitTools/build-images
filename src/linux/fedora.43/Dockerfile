FROM fedora:43

RUN dnf install -y findutils git glibc-langpack-en \
    krb5-libs libicu openssl-libs wget zlib \
    && rm -rf /tmp/* \
    && groupadd --gid 10001 gittools \
    && useradd --uid 10001 --gid 10001 --create-home --shell /bin/bash gittools

USER gittools

