ARG DOTNET_VERSION='5.0'

FROM fedora:33
LABEL maintainers="GitTools Maintainers"

ARG DOTNET_VERSION

RUN dnf install wget git -y \
    && rm -rf /tmp/*

WORKDIR /app
