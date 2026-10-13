FROM alpine:3.23
# Pre-requisits
RUN apk --no-cache add bash ca-certificates git icu-data-full icu-libs krb5 libgcc libssl3 libstdc++ tzdata wget zlib \
    && addgroup -g 10001 -S gittools \
    && adduser -u 10001 -S -D -G gittools gittools \
    && mkdir -p /home/gittools/app \
    && chown -R 10001:10001 /home/gittools

ENV HOME=/home/gittools
WORKDIR /home/gittools/app
USER 10001:10001
