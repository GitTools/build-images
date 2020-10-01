ARG DOTNET_VERSION='2.1'
ARG DOTNET_VARIANT='sdk'
ARG TAG='alpine3.10'

FROM mcr.microsoft.com/dotnet/core/$DOTNET_VARIANT:$DOTNET_VERSION-$TAG
LABEL maintainers="GitTools Maintainers"

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

RUN apk add --no-cache git

WORKDIR /app
