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
COPY ["src/CartService/query/AppleShop.cart.queryApi/AppleShop.cart.queryApi.csproj", "src/CartService/query/AppleShop.cart.queryApi/"]
COPY ["src/CartService/AppleShop.cart.Persistence/AppleShop.cart.Persistence.csproj", "src/CartService/AppleShop.cart.Persistence/"]
COPY ["src/CartService/AppleShop.cart.Domain/AppleShop.cart.Domain.csproj", "src/CartService/AppleShop.cart.Domain/"]
COPY ["src/AppleShop.Share/AppleShop.Share.csproj", "src/AppleShop.Share/"]
COPY ["src/CartService/query/AppleShop.cart.queryApplication/AppleShop.cart.queryApplication.csproj", "src/CartService/query/AppleShop.cart.queryApplication/"]
COPY ["src/CartService/query/AppleShop.cart.queryInfrastructure/AppleShop.cart.queryInfrastructure.csproj", "src/CartService/query/AppleShop.cart.queryInfrastructure/"]
RUN dotnet restore "./src/CartService/query/AppleShop.cart.queryApi/AppleShop.cart.queryApi.csproj"
COPY . .
WORKDIR "/src/src/CartService/query/AppleShop.cart.queryApi"
RUN dotnet build "./AppleShop.cart.queryApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AppleShop.cart.queryApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppleShop.cart.queryApi.dll"]