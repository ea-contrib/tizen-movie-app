ARG SLN_IMAGE
FROM ${SLN_IMAGE} AS publish

FROM mcr.microsoft.com/dotnet/core/runtime:3.1.3-alpine3.11 AS final

ARG BUILD_CONFIGURATION=Debug
WORKDIR /app

COPY --from=publish  /src/TMA.PrincipalService/bin/${BUILD_CONFIGURATION}/netcoreapp3.1/publish .

ENTRYPOINT ["dotnet", "TMA.PrincipalService.dll"]