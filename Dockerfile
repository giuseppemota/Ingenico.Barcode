FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore Ingenico.Barcode.sln
RUN dotnet publish src/Ingenico.Barcode.API/Ingenico.Barcode.API.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=10000
EXPOSE 10000

COPY --from=build /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "Ingenico.Barcode.API.dll"]
