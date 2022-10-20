FROM alpine:3.15
LABEL maintainers="GitTools Maintainers"

USER root
# Pre-requisits
RUN apk add bash wget git \
    icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib \
    && apk update
