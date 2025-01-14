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
COPY ["src/ProductService/query/AppleShop.product.queryApi/AppleShop.product.queryApi.csproj", "src/ProductService/query/AppleShop.product.queryApi/"]
COPY ["src/ProductService/AppleShop.product.Persistence/AppleShop.product.Persistence.csproj", "src/ProductService/AppleShop.product.Persistence/"]
COPY ["src/ProductService/AppleShop.product.Domain/AppleShop.product.Domain.csproj", "src/ProductService/AppleShop.product.Domain/"]
COPY ["src/AppleShop.Share/AppleShop.Share.csproj", "src/AppleShop.Share/"]
COPY ["src/ProductService/query/AppleShop.product.queryApplication/AppleShop.product.queryApplication.csproj", "src/ProductService/query/AppleShop.product.queryApplication/"]
COPY ["src/ProductService/query/AppleShop.product.queryInfrastructure/AppleShop.product.queryInfrastructure.csproj", "src/ProductService/query/AppleShop.product.queryInfrastructure/"]
RUN dotnet restore "./src/ProductService/query/AppleShop.product.queryApi/AppleShop.product.queryApi.csproj"
COPY . .
WORKDIR "/src/src/ProductService/query/AppleShop.product.queryApi"
RUN dotnet build "./AppleShop.product.queryApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AppleShop.product.queryApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppleShop.product.queryApi.dll"]