ARG DOTNET_VERSION='10.0'
ARG TAG='alpine.3.23'

FROM gittools/deps:$TAG

# Elevate only while assembling the SDK/runtime image. The generator restores USER 10001:10001.
USER root

ENV PATH=$PATH:/usr/local/bin:/home/gittools/.dotnet/tools \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true \
    DOTNET_NOLOGO=true \
    DOTNET_ROOT=/usr/local/bin

ARG DOTNET_VERSION

RUN git config --system --add safe.directory '*'

WORKDIR /home/gittools/app
USER 10001:10001
