FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BattleshipGame.Worker/BattleshipGame.Worker.csproj", "BattleshipGame.Worker/"]
RUN dotnet restore "BattleshipGame.Worker/BattleshipGame.Worker.csproj"
COPY . .
WORKDIR "/src/BattleshipGame.Worker"
RUN dotnet build "BattleshipGame.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BattleshipGame.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BattleshipGame.Worker.dll"]
