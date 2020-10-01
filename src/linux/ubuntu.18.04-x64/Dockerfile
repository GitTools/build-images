ARG DOTNET_VERSION='2.1'
ARG DOTNET_VARIANT='sdk'
ARG TAG='bionic'

FROM mcr.microsoft.com/dotnet/core/$DOTNET_VARIANT:$DOTNET_VERSION-$TAG
LABEL maintainers="GitTools Maintainers"

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

RUN apt-get update \
    && apt-get install -y git \
    && rm -rf /var/lib/apt/lists/* 

WORKDIR /app
