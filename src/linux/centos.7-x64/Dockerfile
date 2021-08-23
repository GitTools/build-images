ARG DOTNET_VERSION='5.0'

FROM centos:7
LABEL maintainers="GitTools Maintainers"

ARG DOTNET_VERSION

# Pre-requisits
RUN rpm -Uvh https://packages.microsoft.com/config/rhel/7/packages-microsoft-prod.rpm \
    && yum update cache

RUN yum install -y wget git && yum clean all && rm -rf /tmp/*

WORKDIR /app
