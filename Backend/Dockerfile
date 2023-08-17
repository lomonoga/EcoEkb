# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ENV DOTNET_URLS=http://+:80
WORKDIR /source
COPY . .
RUN dotnet restore "./src/EcoEkb.Backend.Api/EcoEkb.Backend.Api.csproj"
RUN dotnet publish "./src/EcoEkb.Backend.Api/EcoEkb.Backend.Api.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80

ENTRYPOINT ["dotnet", "EcoEkb.Backend.Api.dll"]
