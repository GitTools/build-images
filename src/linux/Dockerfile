ARG DOTNET_VERSION='8.0'
ARG TAG='alpine.3.23'

FROM gittools/deps:$TAG

ENV PATH=$PATH:/usr/local/bin:/home/gittools/.dotnet/tools \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true \
    DOTNET_NOLOGO=true \
    DOTNET_ROOT=/usr/local/bin

ARG DOTNET_VERSION

RUN git config --global --add safe.directory '*'

WORKDIR /home/gittools/app
