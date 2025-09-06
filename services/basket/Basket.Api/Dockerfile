# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/basket/Basket.Api/Basket.Api.csproj", "services/basket/Basket.Api/"]
COPY ["services/basket/Basket.Infrastructure/Basket.Infrastructure.csproj", "services/basket/Basket.Infrastructure/"]
COPY ["services/basket/Basket.Application/Basket.Application.csproj", "services/basket/Basket.Application/"]
COPY ["services/basket/Basket.Core/Basket.Core.csproj", "services/basket/Basket.Core/"]
#Rabbit MQ
COPY ["Infrastructure/EventBus.Messages/EventBus.Messages.csproj", "Infrastructure/EventBus.Messages/"]
RUN dotnet restore "./services/basket/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/services/basket/Basket.Api"
RUN dotnet build "./Basket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Basket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]