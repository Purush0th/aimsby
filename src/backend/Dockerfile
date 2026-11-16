# Aimsby API — build context is the repo root:
#   docker build -f src/backend/Dockerfile .
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# The build rules (warnings as errors, analyzer levels) and the analyzer settings they rely on:
# without .editorconfig the rules it turns off would fail the build here.
COPY .editorconfig ./
COPY src/backend/Directory.Build.props src/backend/

# Copy only project files first so `restore` is cached until dependencies change.
COPY src/backend/Aimsby.Api/Aimsby.Api.csproj            src/backend/Aimsby.Api/
COPY src/backend/Aimsby.Application/Aimsby.Application.csproj src/backend/Aimsby.Application/
COPY src/backend/Aimsby.Domain/Aimsby.Domain.csproj      src/backend/Aimsby.Domain/
COPY src/backend/Aimsby.Infrastructure/Aimsby.Infrastructure.csproj src/backend/Aimsby.Infrastructure/
RUN dotnet restore src/backend/Aimsby.Api/Aimsby.Api.csproj

COPY src/backend/ src/backend/
# The release this image is, from the tag (.github/workflows/release.yml). The API reports it, and
# the update check compares it with the published releases; a local build is "0.0.0-dev".
ARG VERSION=0.0.0-dev
RUN dotnet publish src/backend/Aimsby.Api/Aimsby.Api.csproj \
    -c Release -o /app/publish --no-restore -p:Version=${VERSION}

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Run as the image's non-root user; the data volume is chowned to it.
ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    Documents__StoragePath=/app/data/docs-storage \
    DataProtection__KeysPath=/app/data/dp-keys

COPY --from=build /app/publish .
# /app/updates is shared with the updater container (deploy/docker-compose.yml): the API writes
# its requests there, so a fresh volume must start out owned by the API's user.
RUN mkdir -p /app/data/docs-storage /app/data/dp-keys /app/updates && chown -R app:app /app/data /app/updates
USER app

EXPOSE 8080
ENTRYPOINT ["dotnet", "Aimsby.Api.dll"]
