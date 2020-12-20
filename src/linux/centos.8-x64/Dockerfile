ARG DOTNET_VERSION='3.1'
ARG DOTNET_VARIANT='sdk'
ARG TAG='8'

FROM centos:$TAG
LABEL maintainers="GitTools Maintainers"

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ENV DOTNET_NOLOGO=true
ARG DOTNET_VERSION
ARG DOTNET_VARIANT

RUN dnf install -y dotnet-$DOTNET_VARIANT-$DOTNET_VERSION.x86_64 \
    && dnf install -y git \
    && rm -rf /tmp/*

WORKDIR /app
