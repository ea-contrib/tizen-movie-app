ARG SLN_IMAGE
FROM ${SLN_IMAGE} AS publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-alpine3.11 AS final


ARG BUILD_CONFIGURATION=Debug
WORKDIR /app
EXPOSE 80
COPY --from=publish /src/TMA.Web/bin/${BUILD_CONFIGURATION}/netcoreapp3.1/publish .

ENTRYPOINT ["dotnet", "TMA.Web.dll"]