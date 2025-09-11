# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
#Expose 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/Catalog/Catalog.Api/Catalog.Api.csproj", "services/Catalog/Catalog.Api/"]
COPY ["services/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "services/Catalog/Catalog.Infrastructure/"]
COPY ["services/Catalog/Catalog.Application/Catalog.Application.csproj", "services/Catalog/Catalog.Application/"]
COPY ["services/Catalog/Catalog.Core/Catalog.Core.csproj", "services/Catalog/Catalog.Core/"]
#Logging
COPY ["Infrastructure/Common.Logging/Common.Logging.csproj","Infrastructure/Common.Logging/"]
RUN dotnet restore "./services/Catalog/Catalog.Api/Catalog.Api.csproj"
COPY . .
WORKDIR "/src/services/Catalog/Catalog.Api"
RUN dotnet build "./Catalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]