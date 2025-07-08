FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ["UrlShortener.Web/UrlShortener.Web.csproj", "UrlShortener.Web/"]
COPY ["UrlShortener.Application/UrlShortener.Application.csproj", "UrlShortener.Application/"]
COPY ["UrlShortener.Persistence/UrlShortener.Persistence.csproj", "UrlShortener.Persistence/"]

RUN dotnet restore "UrlShortener.Web/UrlShortener.Web.csproj"

COPY . . 

RUN dotnet publish "UrlShortener.Web/UrlShortener.Web.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app
EXPOSE 7000

COPY --from=build /app/publish . 

ENTRYPOINT ["dotnet", "UrlShortener.Web.dll"]