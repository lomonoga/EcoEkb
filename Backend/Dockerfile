# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./src/EcoEkb.Backend.Api/EcoEkb.Backend.Api.csproj"
RUN dotnet publish "./src/EcoEkb.Backend.Api/EcoEkb.Backend.Api.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "EcoEkb.Backend.Api.dll"]