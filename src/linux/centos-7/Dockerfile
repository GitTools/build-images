ARG DOTNET_VERSION='2.1'
ARG DOTNET_VARIANT='sdk'
ARG TAG='7'

FROM centos:$TAG
LABEL maintainers="GitTools Maintainers"

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ARG DOTNET_VERSION
ARG DOTNET_VARIANT

RUN rpm -Uvh https://packages.microsoft.com/config/rhel/7/packages-microsoft-prod.rpm \
    && yum update cache

RUN yum install -y dotnet-$DOTNET_VARIANT-$DOTNET_VERSION.x86_64 \
    && yum clean all \
    && rm -rf /tmp/*

WORKDIR /app
