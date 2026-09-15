FROM quay.io/centos/centos:stream10

# Pre-requisits
RUN dnf install -y ca-certificates git glibc glibc-langpack-en \
    krb5-libs libgcc libicu libstdc++ openssl-libs tzdata wget zlib \
    && rm -rf /tmp/*
