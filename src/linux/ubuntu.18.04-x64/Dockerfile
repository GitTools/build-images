FROM ubuntu:18.04
LABEL maintainers="GitTools Maintainers"

RUN apt-get update \
    && apt-get install -y wget apt-transport-https \
    && wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb

RUN apt-get update \
    && apt-get install -y git \
    && rm -rf /var/lib/apt/lists/*
