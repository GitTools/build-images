FROM quay.io/centos/centos:stream8

RUN sed -i 's/mirrorlist/#mirrorlist/g' /etc/yum.repos.d/CentOS-* \
    && sed -i 's|#baseurl=http://mirror.centos.org|baseurl=http://vault.centos.org|g' /etc/yum.repos.d/CentOS-*
# Pre-requisits
RUN dnf install -y wget git \
    krb5-libs libicu openssl-libs zlib glibc-langpack-en \
    && rm -rf /tmp/*
