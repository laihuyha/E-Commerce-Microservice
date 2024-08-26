# Base image with SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution and project files
COPY E-Commerce-Microservice.sln .
COPY src/Services/Catalog.API/Catalog.API.csproj src/Services/Catalog.API/Catalog.API.csproj
COPY src/BuildingBlocks/BuildingBlocks.csproj src/BuildingBlocks/BuildingBlocks.csproj

# Restore dependencies
RUN dotnet restore E-Commerce-Microservice.sln

# Build the project
COPY src/Services/Catalog.API src/Services/Catalog.API
COPY src/BuildingBlocks src/BuildingBlocks
WORKDIR /app/src/Services/Catalog.API
RUN dotnet build -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime-only base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
