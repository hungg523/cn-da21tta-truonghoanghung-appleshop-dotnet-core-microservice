# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/InventoryService/command/AppleShop.inventory.commandApi/AppleShop.inventory.commandApi.csproj", "src/InventoryService/command/AppleShop.inventory.commandApi/"]
COPY ["src/InventoryService/AppleShop.inventory.Persistence/AppleShop.inventory.Persistence.csproj", "src/InventoryService/AppleShop.inventory.Persistence/"]
COPY ["src/InventoryService/AppleShop.inventory.Domain/AppleShop.inventory.Domain.csproj", "src/InventoryService/AppleShop.inventory.Domain/"]
COPY ["src/AppleShop.Share/AppleShop.Share.csproj", "src/AppleShop.Share/"]
COPY ["src/InventoryService/command/AppleShop.inventory.commandApplication/AppleShop.inventory.commandApplication.csproj", "src/InventoryService/command/AppleShop.inventory.commandApplication/"]
COPY ["src/InventoryService/command/AppleShop.inventory.commandInfrastructure/AppleShop.inventory.commandInfrastructure.csproj", "src/InventoryService/command/AppleShop.inventory.commandInfrastructure/"]
RUN dotnet restore "./src/InventoryService/command/AppleShop.inventory.commandApi/AppleShop.inventory.commandApi.csproj"
COPY . .
WORKDIR "/src/src/InventoryService/command/AppleShop.inventory.commandApi"
RUN dotnet build "./AppleShop.inventory.commandApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AppleShop.inventory.commandApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppleShop.inventory.commandApi.dll"]