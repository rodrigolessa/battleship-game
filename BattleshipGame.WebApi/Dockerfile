FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BattleshipGame.WebApi/BattleshipGame.WebApi.csproj", "BattleshipGame.WebApi/"]
RUN dotnet restore "BattleshipGame.WebApi/BattleshipGame.WebApi.csproj"
COPY . .
WORKDIR "/src/BattleshipGame.WebApi"
RUN dotnet build "BattleshipGame.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BattleshipGame.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BattleshipGame.WebApi.dll"]
