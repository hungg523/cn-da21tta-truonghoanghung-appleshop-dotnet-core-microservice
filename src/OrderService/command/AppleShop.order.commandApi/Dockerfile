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
COPY ["src/OrderService/command/AppleShop.order.commandApi/AppleShop.order.commandApi.csproj", "src/OrderService/command/AppleShop.order.commandApi/"]
COPY ["src/OrderService/AppleShop.order.Persistence/AppleShop.order.Persistence.csproj", "src/OrderService/AppleShop.order.Persistence/"]
COPY ["src/OrderService/AppleShop.order.Domain/AppleShop.order.Domain.csproj", "src/OrderService/AppleShop.order.Domain/"]
COPY ["src/AppleShop.Share/AppleShop.Share.csproj", "src/AppleShop.Share/"]
COPY ["src/OrderService/command/AppleShop.order.commandApplication/AppleShop.order.commandApplication.csproj", "src/OrderService/command/AppleShop.order.commandApplication/"]
COPY ["src/OrderService/command/AppleShop.order.commandInfrastructure/AppleShop.order.commandInfrastructure.csproj", "src/OrderService/command/AppleShop.order.commandInfrastructure/"]
RUN dotnet restore "./src/OrderService/command/AppleShop.order.commandApi/AppleShop.order.commandApi.csproj"
COPY . .
WORKDIR "/src/OrderService/command/AppleShop.order.commandApi"
RUN dotnet build "./AppleShop.order.commandApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AppleShop.order.commandApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppleShop.order.commandApi.dll"]