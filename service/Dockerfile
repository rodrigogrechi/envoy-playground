FROM envoyproxy/envoy:v1.20.1

COPY ./envoy.yaml /etc/envoy.yaml
RUN chmod go+r /etc/envoy.yaml

ENV \
    DOTNET_GENERATE_ASPNET_CERTIFICATE=false \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    NUGET_XMLDOC_MODE=skip \
    POWERSHELL_DISTRIBUTION_CHANNEL=PSDocker-DotnetCoreSDK-Ubuntu-18.04-arm64

# Install .NET CLI dependencies
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        libc6 \
        libgcc1 \
        libgssapi-krb5-2 \
        libicu60 \
        libssl1.1 \
        libstdc++6 \
        zlib1g \
        curl \
    && rm -rf /var/lib/apt/lists/*

# Install .NET SDK
RUN dotnet_sdk_version=6.0.101 \
    && curl -fSL --output dotnet.tar.gz https://dotnetcli.azureedge.net/dotnet/Sdk/$dotnet_sdk_version/dotnet-sdk-$dotnet_sdk_version-linux-arm64.tar.gz \
    && dotnet_sha512='04cd89279f412ae6b11170d1724c6ac42bb5d4fae8352020a1f28511086dd6d6af2106dd48ebe3b39d312a21ee8925115de51979687a9161819a3a29e270a954' \
    && echo "$dotnet_sha512  dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -ozxf dotnet.tar.gz -C /usr/share/dotnet \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

# Create a .NET webapi sample
RUN dotnet new webapi -o service \
    && cd service \
    && dotnet restore \
    && dotnet publish -c release -o /app --no-restore

ENV ASPNETCORE_URLS http://*:8080

WORKDIR /app

COPY start-service.sh .

ENTRYPOINT ["/bin/sh", "start-service.sh"]