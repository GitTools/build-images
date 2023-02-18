FROM fedora:36
LABEL maintainers="GitTools Maintainers"

RUN rpm --import https://packages.microsoft.com/keys/microsoft.asc \
    && dnf install -y wget git findutils \
    krb5-libs libicu openssl-libs zlib \
    && wget -q -O /etc/yum.repos.d/microsoft-prod.repo https://packages.microsoft.com/config/fedora/36/prod.repo \
    && rm -rf /tmp/*
