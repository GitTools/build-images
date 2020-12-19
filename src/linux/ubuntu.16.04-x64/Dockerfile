ARG DOTNET_VERSION='3.1'
ARG DOTNET_VARIANT='sdk'
ARG TAG='16.04'

FROM ubuntu:$TAG
LABEL maintainers="GitTools Maintainers"

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ARG DOTNET_VERSION
ARG DOTNET_VARIANT

RUN apt-get update \
    && apt-get install -y wget apt-transport-https \
    && wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb

RUN apt-get update \
    && apt-get install -y dotnet-$DOTNET_VARIANT-$DOTNET_VERSION \
    && apt-get install -y git \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
