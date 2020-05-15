#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TMA.IdentityService/TMA.IdentityService.csproj", "TMA.IdentityService/"]
COPY ["TMA.MessageBus/TMA.MessageBus.csproj", "TMA.MessageBus/"]
COPY ["TMA.Configuration/TMA.Configuration.csproj", "TMA.Configuration/"]
COPY ["TMA.Identity/TMA.Identity.csproj", "TMA.Identity/"]
COPY ["TMA.Common/TMA.Common.csproj", "TMA.Common/"]
COPY ["TMA.Contracts/TMA.Contracts.csproj", "TMA.Contracts/"]
RUN dotnet restore "TMA.IdentityService/TMA.IdentityService.csproj"
COPY . .
WORKDIR "/src/TMA.IdentityService"
RUN dotnet build "TMA.IdentityService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TMA.IdentityService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TMA.IdentityService.dll"]
