FROM quay.io/centos/centos:stream9@sha256:5665c076f25172d372d6fb7934f49f9f99b36aa6117afd1935186ccb9fb88cb0

# Pre-requisits
RUN dnf install -y git glibc-langpack-en \
    krb5-libs libicu openssl-libs wget zlib \
    && rm -rf /tmp/*
