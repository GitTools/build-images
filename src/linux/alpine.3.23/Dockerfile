FROM alpine:3.23
# Pre-requisits
RUN apk update && apk --no-cache add bash ca-certificates-bundle git icu-libs libgcc libssl3 libstdc++ wget zlib \
	&& addgroup -g 10001 -S gittools \
	&& adduser -u 10001 -S -D -G gittools gittools

USER gittools

