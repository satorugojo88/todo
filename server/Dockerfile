# Multi Architecture

# --- Stage 1: Build ---
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

# Docker automatically populates this variable with 'amd64' or 'arm64'
ARG TARGETARCH

# 1. Copy project and restore targeting target architecture
COPY ["./", "."]
RUN if [ "$TARGETARCH" = "amd64" ]; then \
            RID="linux-musl-x64"; \
        else \
            RID="linux-musl-arm64"; \
        fi && \
    dotnet restore "api/api.csproj"  --runtime $RID

# 2. Copy the rest of the source
#COPY . .

# 3. Publish 
RUN if [ "$TARGETARCH" = "amd64" ]; then \
             RID="linux-musl-x64"; \
         else \
             RID="linux-musl-arm64"; \
         fi && \
    dotnet publish "api/api.csproj" \
    -c Release \
    -o /app/publish \
    --runtime $RID \
    --self-contained true \
    --no-restore \
    /p:PublishSingleFile=true

# --- Stage 2: Runtime ---
FROM mcr.microsoft.com/dotnet/runtime-deps:10.0-alpine
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080

USER app

ENTRYPOINT ["./api"]