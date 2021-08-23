ARG DOTNET_VERSION='5.0'

FROM alpine:3.13
LABEL maintainers="GitTools Maintainers"

USER root
ARG DOTNET_VERSION

# Pre-requisits
RUN apk add bash icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib wget git && apk update

WORKDIR /app
