# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
RUN mkdir -p /app/data && chown -R $APP_UID:$APP_UID /app/data
USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LojaGraphQL.Api/LojaGraphQL.Api.csproj", "LojaGraphQL.Api/"]
RUN dotnet restore "LojaGraphQL.Api/LojaGraphQL.Api.csproj"
COPY . .
WORKDIR "/src/LojaGraphQL.Api"
RUN dotnet build "LojaGraphQL.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build \
    --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LojaGraphQL.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-build \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_HTTP_PORTS=8080
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/loja-graphql.db"
ENTRYPOINT ["dotnet", "LojaGraphQL.Api.dll"]
