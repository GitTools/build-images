FROM fedora:43@sha256:49b5f0ab05516b1a8168c79f295da65e8d8db1cae275a4bec007c0b88cd19ffb

RUN dnf install -y findutils git glibc-langpack-en \
    krb5-libs libicu openssl-libs wget zlib \
    && rm -rf /tmp/*
