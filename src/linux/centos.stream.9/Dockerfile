FROM quay.io/centos/centos:stream9@sha256:9432fef2ceeff573ac2fae04778afbeb955f3d31b2c0fae4f3d15f56d3473f24

# Pre-requisits
RUN dnf install -y git glibc-langpack-en \
    krb5-libs libicu openssl-libs wget zlib \
    && rm -rf /tmp/*
