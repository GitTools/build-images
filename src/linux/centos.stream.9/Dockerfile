FROM quay.io/centos/centos:stream9

# Pre-requisits
RUN dnf install -y git glibc-langpack-en \
    krb5-libs libicu openssl-libs wget zlib \
    && rm -rf /tmp/*
