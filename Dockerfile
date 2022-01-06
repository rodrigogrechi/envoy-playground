FROM envoyproxy/envoy:v1.20.1

ARG FOLDER
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

# Install .NET Core SDK
RUN dotnet_sdk_version=3.1.416 \
    && curl -fSL --output dotnet.tar.gz https://dotnetcli.azureedge.net/dotnet/Sdk/$dotnet_sdk_version/dotnet-sdk-$dotnet_sdk_version-linux-arm64.tar.gz \
    && dotnet_sha512='0065c7afb129b1a0e0c11703309f3b45cf9a3c0ea156247f7cc61555f21c37054f215eb77add509dad77b1d388a4e6c585f8a8016109f31c5b64184b25e2c407' \
    && echo "$dotnet_sha512  dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -ozxf dotnet.tar.gz -C /usr/share/dotnet \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

WORKDIR /project

COPY ./${FOLDER} .
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

ENV ASPNETCORE_URLS http://*:8080

WORKDIR /app

COPY start-service.sh .

ENTRYPOINT ["/bin/sh", "start-service.sh"]