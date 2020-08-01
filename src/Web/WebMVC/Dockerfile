#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY ["TheBroker.sln", "."]
COPY ["docker-compose.dcproj", "docker-compose.dcproj""]
COPY ["BuildingBlocks/WebHostCustomization/WebHost.Customization/WebHost.Customization.csproj", "BuildingBlocks/WebHostCustomization/WebHost.Customization/"]
COPY ["BungBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "BuildingBlocks/EventBus/EventBusRabbitMQ/"]
COPY ["BuildingBlocks/EventBus/EventBusServiceBus/EventBusServiceBus.csproj", "BuildingBlocks/EventBus/EventBusServiceBus/"]
COPY ["BuildingBlocks/EventBus/IntegrationEventLogEF/IntegrationEventLogEF.csproj", "BuildingBlocks/EventBus/IntegrationEventLogEF/"]
COPY ["BuildingBlocks/Devspaces.Support/Devspaces.Support.csproj", "BuildingBlocks/Devspaces.Support/"]
COPY ["BuildingBlocks/EventBus/EventBus/EventBus.csproj", "BuildingBlocks/EventBus/EventBus/"]
COPY ["ApiGateways/Web.Bff/WebClientAgg/WebClientAgg.csproj", "ApiGateways/Web.Bff/WebClientAgg/"]
COPY ["Services/Identity/Identity.API/Identity.API.csproj", "Services/Identity/Identity.API/"]
COPY ["Services/Apartment/Apartment.API/Apartment.API.csproj", "Services/Apartment/Apartment.API/"]
COPY ["Services/Owners/Owners.API/Owners.API.csproj", "Services/Owners/Owners.API/"]
COPY ["Web/WebMVC/WebMVC.csproj", "src/Web/WebMVC/"]

RUN dotnet restore "TheBroker.sln"
COPY . .
WORKDIR /src/Web/WebMVC
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebMVC.dll"]