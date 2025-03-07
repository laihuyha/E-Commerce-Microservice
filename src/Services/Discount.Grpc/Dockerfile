FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY E-Commerce-Microservice.sln E-Commerce-Microservice.sln
COPY ["src/BuildingBlocks/BuildingBlocks.csproj", "src/BuildingBlocks/BuildingBlocks.csproj"]
COPY ["src/Services/Catalog.API/Catalog.API.csproj", "src/Services/Catalog.API/Catalog.API.csproj"]
COPY ["src/Services/Basket.API/Basket.API.csproj" ,"src/Services/Basket.API/Basket.API.csproj"]
COPY ["src/Services/Discount.Grpc/Discount.Grpc.csproj", "src/Services/Discount.Grpc/"]
RUN dotnet restore E-Commerce-Microservice.sln

COPY ["src/BuildingBlocks", "src/BuildingBlocks"]
COPY ["src/Services/Discount.Grpc", "src/Services/Discount.Grpc"]

WORKDIR "/app/src/Services/Discount.Grpc"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/src/out /p:UseAppHost=false
RUN dotnet publish -c Release -o /app/src/out \
    && mkdir -p /app/src/out/certs \
    && dotnet dev-certs https --clean \
    && dotnet dev-certs https -ep /app/src/out/certs/aspnetapp.pfx -p "simplePassword" \
    && dotnet dev-certs https --trust

FROM base AS final
WORKDIR /app
COPY --from=publish /app/src/out .
USER root
RUN mkdir -p /app/.aspnet/https/ && \
    cp /app/certs/aspnetapp.pfx /app/.aspnet/https/ && \
    chown -R $APP_UID:$APP_UID /app/.aspnet
USER $APP_UID
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/.aspnet/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=simplePassword
ENTRYPOINT ["dotnet", "Discount.Grpc.dll"]
