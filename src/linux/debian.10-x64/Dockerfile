ARG DOTNET_VERSION='3.1'
ARG DOTNET_VARIANT='sdk'
ARG TAG='10'

FROM debian:$TAG
LABEL maintainers="GitTools Maintainers"

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ENV DOTNET_NOLOGO=true
ARG DOTNET_VERSION
ARG DOTNET_VARIANT

RUN apt-get update \
    && apt-get install -y wget gpg apt-transport-https \
    && wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg \
    && mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/ \
    && wget -q https://packages.microsoft.com/config/debian/10/prod.list \
    && mv prod.list /etc/apt/sources.list.d/microsoft-prod.list

# Install ca-certificates from bullseye repository: https://github.com/NuGet/Announcements/issues/49
RUN echo "deb http://deb.debian.org/debian bullseye main" >> /etc/apt/sources.list \
    && apt-get update \
    && apt-get install -y --no-install-recommends \
         ca-certificates \
    && rm -rf /var/lib/apt/lists/* \
    && sed -i '$ d' /etc/apt/sources.list

RUN apt-get update \
    && apt-get install -y dotnet-$DOTNET_VARIANT-$DOTNET_VERSION \
    && apt-get install -y git \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
