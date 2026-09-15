FROM fedora:44

RUN dnf install -y ca-certificates findutils git glibc glibc-langpack-en \
    krb5-libs libgcc libicu libstdc++ openssl-libs tzdata wget zlib \
    && rm -rf /tmp/*
