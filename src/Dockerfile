FROM debian:buster

ARG MONOGAME_VERSION=3.7.1
ENV MONOGAME_VERSION $MONOGAME_VERSION

RUN apt-get update && \
    apt-get install -y apt-transport-https wget gpg && \
    wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg && \
    mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/ && \
    wget -q https://packages.microsoft.com/config/debian/10/prod.list && \
    mv prod.list /etc/apt/sources.list.d/microsoft-prod.list && \
    chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg && \
    chown root:root /etc/apt/sources.list.d/microsoft-prod.list && \
    apt-get update && \
    apt-get install -y dotnet-sdk-3.1 dotnet-runtime-3.1 

# Download monogame from the official repo
RUN apt-get update \
    && apt-get install -y --no-install-recommends wget gtk-sharp3 \
    && wget -O monogame-sdk.run https://github.com/MonoGame/MonoGame/releases/download/v$MONOGAME_VERSION/monogame-sdk.run \
    && chmod +x monogame-sdk.run \
    && yes | ./monogame-sdk.run \
    && apt-get remove -y wget \
    && apt-get autoremove -y \
    && rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*

RUN dotnet tool install -g dotnet-reportgenerator-globaltool
ENV PATH="/root/.dotnet/tools/:${PATH}" 

# WORKDIR /work
COPY ./fonts /usr/share/fonts

# RUN 

CMD [ "bash" ]