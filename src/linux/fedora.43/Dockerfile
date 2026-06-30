FROM fedora:43@sha256:762d73ba1c455232b0272c5d445a34f36c4b9f421cbc05ce8102552325b6a222

RUN dnf install -y findutils git glibc-langpack-en \
    krb5-libs libicu openssl-libs wget zlib \
    && rm -rf /tmp/*
