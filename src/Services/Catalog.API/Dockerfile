FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

# copy all .csproj file and restore as distinct layer
COPY E-Commerce-Microservice.sln E-Commerce-Microservice.sln

COPY src/Services/Catalog.API/Catalog.API.csproj src/Services/Catalog.API/Catalog.API.csproj

COPY src/BuildingBlocks/BuildingBlocks.csproj src/BuildingBlocks/BuildingBlocks.csproj

# restore package
RUN dotnet restore E-Commerce-Microservice.sln

# copy the app folders over
COPY src/Services/Catalog.API src/Services/Catalog.API

COPY src/BuildingBlocks src/BuildingBlocks

WORKDIR /app/src/Services/Catalog.API

RUN dotnet publish -c Release -o /app/src/out

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]