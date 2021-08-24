FROM fedora:33
LABEL maintainers="GitTools Maintainers"

RUN dnf install -y wget git  \
    krb5-libs libicu openssl-libs zlib \
    && rm -rf /tmp/*
