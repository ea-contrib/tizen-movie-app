FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201-buster AS build
ARG BUILD_CONFIGURATION=Debug

WORKDIR /src

COPY ./*.sln ./*.dcproj ./

COPY ./Tests/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./Tests/${file%.*}/ && mv $file ./Tests/${file%.*}/; done;

COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done;


RUN dotnet restore "TMA.sln"

COPY . .

RUN dotnet publish "TMA.sln" -c ${BUILD_CONFIGURATION} --no-restore