FROM opensuse/leap:15.2
LABEL maintainers="GitTools Maintainers"

RUN zypper install -y libicu-devel git-core \
    && rpm --import https://packages.microsoft.com/keys/microsoft.asc \
    && wget https://packages.microsoft.com/config/opensuse/15/prod.repo \
    && mv prod.repo /etc/zypp/repos.d/microsoft-prod.repo \
    && chown root:root /etc/zypp/repos.d/microsoft-prod.repo \
    && rm -rf /tmp/*
