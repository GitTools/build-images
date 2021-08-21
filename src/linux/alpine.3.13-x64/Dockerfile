ARG DOTNET_VERSION='3.1'
ARG DOTNET_VARIANT='sdk'
ARG TAG='alpine3.13'

FROM mcr.microsoft.com/dotnet/$DOTNET_VARIANT:$DOTNET_VERSION-$TAG
LABEL maintainers="GitTools Maintainers"

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ENV DOTNET_NOLOGO=true

RUN apk add --no-cache git

WORKDIR /app
