FROM quay.io/centos/centos:stream10
# Root is intentional for downstream GitVersion image builds; see README.md#container-user-policy.

# Pre-requisits
RUN dnf install -y ca-certificates git glibc glibc-langpack-en \
    krb5-libs libgcc libicu libstdc++ openssl-libs tzdata wget zlib \
    && rm -rf /tmp/*
