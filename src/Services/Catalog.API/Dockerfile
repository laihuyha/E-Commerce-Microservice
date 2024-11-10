FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# copy all .csproj file and restore as distinct layer
COPY E-Commerce-Microservice.sln E-Commerce-Microservice.sln

COPY src/Services/Catalog.API/Catalog.API.csproj src/Services/Catalog.API/Catalog.API.csproj
COPY src/Services/Basket.API/Basket.API.csproj src/Services/Basket.API/Basket.API.csproj
COPY src/Services/Discount.Grpc/Discount.Grpc.csproj src/Services/Discount.Grpc/Discount.Grpc.csproj
COPY src/BuildingBlocks/BuildingBlocks.csproj src/BuildingBlocks/BuildingBlocks.csproj

# restore package
RUN dotnet restore E-Commerce-Microservice.sln

# copy the app folders over
COPY src/Services/Catalog.API src/Services/Catalog.API

COPY src/BuildingBlocks src/BuildingBlocks

WORKDIR /app/src/Services/Catalog.API

RUN dotnet publish -c Release -o /app/src/out

# Fix run https error on docker
# Ref: https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-8.0
# Generate the development certificate in the build stage
RUN dotnet publish -c Release -o /app/src/out \
    && mkdir -p /app/certs \
    && dotnet dev-certs https --clean \
    && dotnet dev-certs https -ep /app/certs/aspnetapp.pfx -p "simplePassword" \
    && dotnet dev-certs https --trust

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
COPY --from=build /app/certs/aspnetapp.pfx /root/.aspnet/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/root/.aspnet/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=simplePassword
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
