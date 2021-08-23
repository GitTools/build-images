ARG DOTNET_VERSION='5.0'

FROM centos:8
LABEL maintainers="GitTools Maintainers"

ARG DOTNET_VERSION

# Pre-requisits
RUN dnf install -y wget git && rm -rf /tmp/*

WORKDIR /app
