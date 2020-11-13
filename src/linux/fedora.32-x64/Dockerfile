ARG DOTNET_VERSION='2.1'
ARG DOTNET_VARIANT='sdk'
ARG TAG='32'

FROM fedora:$TAG
LABEL maintainers="GitTools Maintainers"

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ARG DOTNET_VERSION
ARG DOTNET_VARIANT

RUN rpm --import https://packages.microsoft.com/keys/microsoft.asc && dnf install wget -y \
    && wget -q -O /etc/yum.repos.d/microsoft-prod.repo https://packages.microsoft.com/config/fedora/30/prod.repo \
    && dnf install -y dotnet-$DOTNET_VARIANT-$DOTNET_VERSION \
    && dnf install -y git \
    && rm -rf /tmp/*

WORKDIR /app
