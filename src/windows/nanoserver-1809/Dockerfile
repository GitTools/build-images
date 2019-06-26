ARG DOTNET_VERSION='2.1'
ARG DOTNET_VARIANT='sdk'

FROM mcr.microsoft.com/dotnet/core/$DOTNET_VARIANT:$DOTNET_VERSION-nanoserver-1809
LABEL maintainers="GitTools Maintainers"

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

WORKDIR /app
