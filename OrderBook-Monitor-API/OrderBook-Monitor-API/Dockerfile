FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY OrderBook-Monitor-API.csproj OrderBook-Monitor-API/

RUN dotnet restore "OrderBook-Monitor-API/OrderBook-Monitor-API.csproj"

COPY . OrderBook-Monitor-API/

WORKDIR /src/OrderBook-Monitor-API

RUN dotnet build "OrderBook-Monitor-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderBook-Monitor-API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderBook-Monitor-API.dll"]