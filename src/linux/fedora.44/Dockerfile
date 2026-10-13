FROM fedora:44
# Root is intentional for downstream GitVersion image builds; see README.md#container-user-policy.

RUN dnf install -y ca-certificates findutils git glibc glibc-langpack-en \
    krb5-libs libgcc libicu libstdc++ openssl-libs tzdata wget zlib \
    && rm -rf /tmp/*
