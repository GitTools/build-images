FROM quay.io/centos/centos:stream8
LABEL maintainers="GitTools Maintainers"

# Pre-requisits
RUN dnf install -y wget git \
    krb5-libs libicu openssl-libs zlib \
    && rm -rf /tmp/*
