#FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
#WORKDIR /src
#COPY ["./", "."]
#RUN dotnet publish "api/api.csproj" \
#    -c Release \
#    -o /app/publish \
#    --runtime linux-musl-x64 \
#    --self-contained true \
#    /p:PublishSingleFile=true
#
#FROM mcr.microsoft.com/dotnet/runtime-deps:10.0-alpine
#WORKDIR /app
#COPY --from=build /app/publish .
#ENV ASPNETCORE_URLS=http://0.0.0.0:8080
#EXPOSE 8080
#
#ENTRYPOINT ["./api"]

# --- Stage 1: Build ---
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

# 1. Copy project and restore targeting alpine-arm64 --runtime linux-musl-arm64 on Surface Pro or Macbook Air
COPY ["./", "."]
RUN dotnet restore "api/api.csproj" --runtime linux-musl-x64  \

# 2. Copy the rest of the source
#COPY . .

# 3. Publish targeting alpine-arm64 --runtime linux-musl-arm64 on Surface Pro or Macbook Air
RUN dotnet publish "api/api.csproj" \
    -c Release \
    -o /app/publish \
    --runtime linux-musl-x64 \
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