FROM debian:buster

ARG MONOGAME_VERSION=3.7.1
ARG DOTNET_VERSION=3.1
ARG DOCFX_VERSION=2.52

ENV MONOGAME_VERSION $MONOGAME_VERSION
ENV DOTNET_VERSION $DOTNET_VERSION
ENV DOCFX_VERSION $DOCFX_VERSION


# Install dotnet 3.1
RUN apt-get update && \
    apt-get install -y apt-transport-https wget gpg && \
    wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg && \
    mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/ && \
    wget -q https://packages.microsoft.com/config/debian/10/prod.list && \
    mv prod.list /etc/apt/sources.list.d/microsoft-prod.list && \
    chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg && \
    chown root:root /etc/apt/sources.list.d/microsoft-prod.list && \
    apt-get update && \
    apt-get install -y dotnet-sdk-$DOTNET_VERSION dotnet-runtime-$DOTNET_VERSION



# Download monogame from the official repo
RUN apt-get update && \
    apt-get install -y --no-install-recommends wget gtk-sharp3 && \
    wget -O monogame-sdk.run https://github.com/MonoGame/MonoGame/releases/download/v$MONOGAME_VERSION/monogame-sdk.run && \
    chmod +x monogame-sdk.run && \
    yes | ./monogame-sdk.run



# Install report generator
RUN dotnet tool install -g dotnet-reportgenerator-globaltool
ENV PATH="/root/.dotnet/tools/:${PATH}" 



# Remove the install tools
RUN apt-get remove -y wget gpg curl git sudo locales unzip \
    && apt-get autoremove -y \
    && rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*

# Copy the necessary fonts
COPY ./fonts /usr/share/fonts

CMD [ "bash" ]