FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy all .csproj files and restore as a distinct layer
COPY E-Commerce-Microservice.sln E-Commerce-Microservice.sln

COPY src/Services/Catalog.API/Catalog.API.csproj src/Services/Catalog.API/Catalog.API.csproj
COPY src/Services/Basket.API/Basket.API.csproj src/Services/Basket.API/Basket.API.csproj
COPY src/Services/KeyCloak.Auth/KeyCloak.Auth.csproj src/Services/KeyCloak.Auth/KeyCloak.Auth.csproj
COPY src/BuildingBlocks/BuildingBlocks.csproj src/BuildingBlocks/BuildingBlocks.csproj

# Restore packages
RUN dotnet restore E-Commerce-Microservice.sln

# Copy the app folders over
COPY src/Services/KeyCloak.Auth src/Services/KeyCloak.Auth

WORKDIR /app/src/Services/KeyCloak.Auth

# Fix run HTTPS error on Docker
# Generate the development certificate in the build stage
RUN dotnet publish -c Release -o /app/src/out \
    && mkdir -p /app/certs \
    && dotnet dev-certs https --clean \
    && dotnet dev-certs https -ep /app/certs/aspnetapp.pfx -p "simplePassword" \
    && dotnet dev-certs https --trust

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
COPY --from=build /app/certs/aspnetapp.pfx /root/.aspnet/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/root/.aspnet/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=simplePassword
ENTRYPOINT ["dotnet", "KeyCloak.Auth.dll"]