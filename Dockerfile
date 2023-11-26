FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY . ./

RUN dotnet restore Summario.sln

WORKDIR /src/Summario.API

RUN dotnet build Summario.API.csproj -c Release -o /app
RUN dotnet publish Summario.API.csproj -c Release -o /app/published

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime 

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app

COPY --from=build /app/published .

STOPSIGNAL SIGINT

ENTRYPOINT ["dotnet", "Summario.API.dll"]
