#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY "TheBroker.sln" "TheBroker.sln"

COPY "src/ApiGateways/Web.Bff/WebClientAgg/WebClientAgg.csproj" "src/ApiGateways/Web.Bff/WebClientAgg/WebClientAgg.csproj"
COPY "src/BuildingBlocks/Devspaces.Support/Devspaces.Support.csproj" "src/BuildingBlocks/Devspaces.Support/Devspaces.Support.csproj"
COPY "src/BuildingBlocks/EventBus/EventBus/EventBus.csproj" "src/BuildingBlocks/EventBus/EventBus/EventBus.csproj"
COPY "src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj" "src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj"
COPY "src/BuildingBlocks/EventBus/EventBusServiceBus/EventBusServiceBus.csproj" "src/BuildingBlocks/EventBus/EventBusServiceBus/EventBusServiceBus.csproj"
COPY "src/BuildingBlocks/EventBus/IntegrationEventLogEF/IntegrationEventLogEF.csproj" "src/BuildingBlocks/EventBus/IntegrationEventLogEF/IntegrationEventLogEF.csproj"
COPY "src/BuildingBlocks/WebHost/WebHost.Customization/WebHost.Customization.csproj" "src/BuildingBlocks/WebHost/WebHost.Customization/WebHost.Customization.csproj"
COPY "src/Services/Apartment/Apartment.API/Apartment.API.csproj" "src/Services/Apartment/Apartment.API/Apartment.API.csproj"
COPY "src/Services/Identity/Identity.API/Identity.API.csproj" "src/Services/Identity/Identity.API/Identity.API.csproj"
COPY "src/Services/Owners/Owners.API/Owners.API.csproj" "src/Services/Owners/Owners.API/Owners.API.csproj"
COPY "src/Services/SignalR/SignalrHub/SignalrHub.csproj" "src/Services/SignalR/SignalrHub/SignalrHub.csproj"
COPY "src/Web/WebMVC/WebMVC.csproj" "src/Web/WebMVC/WebMVC.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

COPY "NuGet.config" "NuGet.config"

RUN dotnet restore "TheBroker.sln" --configfile "/src/NuGet.config" --disable-parallel
RUN rm /src/NuGet.config
COPY . .
WORKDIR /src/src/ApiGateways/Web.Bff/WebClientAgg
RUN dotnet publish "WebClientAgg.csproj" --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebClientAgg.dll"]