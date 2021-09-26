FROM centos:7
LABEL maintainers="GitTools Maintainers"

# Pre-requisits
RUN rpm -Uvh https://packages.microsoft.com/config/rhel/7/packages-microsoft-prod.rpm \
    && yum update cache

RUN yum install -y wget git \
    krb5-libs libicu libstdc++6 openssl-libs zlib \
    && yum clean all && rm -rf /tmp/*
