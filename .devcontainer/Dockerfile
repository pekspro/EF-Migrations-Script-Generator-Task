FROM mcr.microsoft.com/dotnet/sdk:10.0 AS base

# Dockerfile for development container supporting .NET SDK, Node.js, and VS Code Remote
ARG NODE_VERSION=24
ARG DOTNET_SDK_VERSION=10.0

ENV DEBIAN_FRONTEND=noninteractive

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

# Create non-root developer user to match vscode default
ARG USER=vscode
ARG USER_UID=2000
ARG USER_GID=2000

RUN groupadd --gid ${USER_GID} ${USER} \
    && useradd --uid ${USER_UID} --gid ${USER_GID} -m -s /bin/bash ${USER} \
    && apt-get update \
    && apt-get install -y sudo bash-completion \
    && echo "${USER} ALL=(ALL) NOPASSWD:ALL" > /etc/sudoers.d/${USER} \
    && chmod 0440 /etc/sudoers.d/${USER} \
    && rm -rf /var/lib/apt/lists/*

USER ${USER}
WORKDIR /workspace

# Ensure dotnet global tools path exists and is on PATH for the non-root user
ENV DOTNET_TOOLS=/home/${USER}/.dotnet/tools
RUN mkdir -p ${DOTNET_TOOLS} && \
    chown -R ${USER}:${USER} /home/${USER}/.dotnet || true

# Configure npm global install directory for non-root user and add to PATH
ENV NPM_CONFIG_PREFIX=/home/${USER}/.npm-global
RUN mkdir -p ${NPM_CONFIG_PREFIX} && chown -R ${USER}:${USER} ${NPM_CONFIG_PREFIX} || true
ENV PATH="${NPM_CONFIG_PREFIX}/bin:${DOTNET_TOOLS}:$PATH"

CMD ["/bin/bash"]
