ARG DOTNET_VERSION='3.1'
ARG TAG='alpine.3.13-x64'

FROM gittools/deps:$TAG
LABEL maintainers="GitTools Maintainers"

ENV PATH=$PATH:/usr/local/bin:/root/.dotnet/tools \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true \
    DOTNET_NOLOGO=true \
    DOTNET_ROOT=/usr/local/bin

ARG DOTNET_VERSION

WORKDIR /app
