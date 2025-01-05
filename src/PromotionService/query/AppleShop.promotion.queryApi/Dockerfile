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
COPY ["src/PromotionService/query/AppleShop.promotion.queryApi/AppleShop.promotion.queryApi.csproj", "src/PromotionService/query/AppleShop.promotion.queryApi/"]
COPY ["src/PromotionService/AppleShop.promotion.Persistence/AppleShop.promotion.Persistence.csproj", "src/PromotionService/AppleShop.promotion.Persistence/"]
COPY ["src/PromotionService/AppleShop.promotion.Domain/AppleShop.promotion.Domain.csproj", "src/PromotionService/AppleShop.promotion.Domain/"]
COPY ["src/AppleShop.Share/AppleShop.Share.csproj", "src/AppleShop.Share/"]
COPY ["src/PromotionService/query/AppleShop.promotion.queryApplication/AppleShop.promotion.queryApplication.csproj", "src/PromotionService/query/AppleShop.promotion.queryApplication/"]
COPY ["src/PromotionService/query/AppleShop.promotion.queryInfrastructure/AppleShop.promotion.queryInfrastructure.csproj", "src/PromotionService/query/AppleShop.promotion.queryInfrastructure/"]
RUN dotnet restore "./src/PromotionService/query/AppleShop.promotion.queryApi/AppleShop.promotion.queryApi.csproj"
COPY . .
WORKDIR "/src/PromotionService/query/AppleShop.promotion.queryApi"
RUN dotnet build "./AppleShop.promotion.queryApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AppleShop.promotion.queryApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppleShop.promotion.queryApi.dll"]