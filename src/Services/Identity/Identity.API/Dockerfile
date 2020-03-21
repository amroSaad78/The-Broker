#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY ["TheBroker.sln", "."]
COPY ["docker-compose.dcproj", "/src/""]
COPY ["src/BuildingBlocks/WebHostCustomization/WebHost.Customization/WebHost.Customization.csproj", "src/BuildingBlocks/WebHostCustomization/WebHost.Customization/"]
COPY ["src/Services/Identity/Identity.API/Identity.API.csproj", "src/Services/Identity/Identity.API/"]
COPY ["src/BuildingBlocks/Devspaces.Support/Devspaces.Support.csproj", "src/BuildingBlocks/Devspaces.Support/"]
COPY ["src/Web/WebMVC/WebMVC.csproj", "src/Web/WebMVC/"]

RUN dotnet restore "TheBroker.sln"
COPY . .
WORKDIR "/src/src/Services/Identity/Identity.API"
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Identity.API.dll"]