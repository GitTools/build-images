FROM centos:8
LABEL maintainers="GitTools Maintainers"

RUN cd /etc/yum.repos.d/
RUN sed -i 's/mirrorlist/#mirrorlist/g' /etc/yum.repos.d/CentOS-*
RUN sed -i 's|#baseurl=http://mirror.centos.org|baseurl=http://vault.centos.org|g' /etc/yum.repos.d/CentOS-*

# Pre-requisits
RUN dnf install -y wget git \
    krb5-libs libicu openssl-libs zlib \
    && rm -rf /tmp/*
