ARG DOTNET_VERSION='3.1'
ARG BASE_IMAGE='gittools/deps'
ARG TAG='alpine.3.13-x64'

FROM $BASE_IMAGE:$TAG
LABEL maintainers="GitTools Maintainers"

ENV PATH=$PATH:/usr/local/bin:/root/.dotnet/tools \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true \
    DOTNET_NOLOGO=true \
    DOTNET_ROOT=/usr/local/bin

ARG DOTNET_VERSION