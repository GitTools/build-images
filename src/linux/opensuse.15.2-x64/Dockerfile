ARG DOTNET_VERSION='3.1'
ARG DOTNET_VARIANT='sdk'
ARG TAG='15.2'

FROM opensuse/leap:$TAG
LABEL maintainers="GitTools Maintainers"

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ARG DOTNET_VERSION
ARG DOTNET_VARIANT

RUN zypper install -y libicu-devel git-core \
    && rpm --import https://packages.microsoft.com/keys/microsoft.asc \
    && wget https://packages.microsoft.com/config/opensuse/15/prod.repo \
    && mv prod.repo /etc/zypp/repos.d/microsoft-prod.repo \
    && chown root:root /etc/zypp/repos.d/microsoft-prod.repo \
    && zypper install dotnet-$DOTNET_VARIANT-$DOTNET_VERSION \
    && rm -rf /tmp/*

WORKDIR /app
