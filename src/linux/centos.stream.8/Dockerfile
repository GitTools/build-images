FROM quay.io/centos/centos:stream8

# Pre-requisits
RUN dnf install -y wget git \
    krb5-libs libicu openssl-libs zlib glibc-langpack-en \
    && rm -rf /tmp/*
