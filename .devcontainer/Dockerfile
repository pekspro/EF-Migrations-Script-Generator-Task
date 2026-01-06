FROM mcr.microsoft.com/dotnet/sdk:10.0 AS base

# Dockerfile for development container supporting .NET SDK, Node.js, and VS Code Remote
ARG NODE_VERSION=24

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        ca-certificates \
        curl \
        git \
        bash \
        apt-transport-https \
        gnupg \
        lsb-release \
    && rm -rf /var/lib/apt/lists/*

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_${NODE_VERSION}.x | bash - \
    && apt-get update \
    && apt-get install -y nodejs \
    && rm -rf /var/lib/apt/lists/*

# Install azure-devops tfx-cli prerequisites (npm package)
RUN npm install -g tfx-cli typescript --no-fund --unsafe-perm

RUN apt-get update \
    && apt-get install -y sudo bash-completion \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /workspace

ENV PATH="$PATH:/root/.dotnet/tools"

CMD ["/bin/bash"]
