ARG DOTNET_VERSION='2.1'
ARG DOTNET_VARIANT='sdk'
ARG TAG='10'

FROM debian:$TAG
LABEL maintainers="GitTools Maintainers"

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ARG DOTNET_VERSION
ARG DOTNET_VARIANT

RUN apt-get update \
    && apt-get install -y wget gpg apt-transport-https \
    && wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg \
    && mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/ \
    && wget -q https://packages.microsoft.com/config/debian/10/prod.list \
    && mv prod.list /etc/apt/sources.list.d/microsoft-prod.list

RUN apt-get update \
    && apt-get install -y dotnet-$DOTNET_VARIANT-$DOTNET_VERSION \
    && apt-get install -y git \
    && rm -rf /var/lib/apt/lists/* 

WORKDIR /app
