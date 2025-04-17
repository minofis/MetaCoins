# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# copy csproj files
COPY MetaCoins/MetaCoins.API/MetaCoins.API.csproj MetaCoins/MetaCoins.API/
COPY MetaCoins/MetaCoins.BLL/MetaCoins.BLL.csproj MetaCoins/MetaCoins.BLL/
COPY MetaCoins/MetaCoins.DAL/MetaCoins.DAL.csproj MetaCoins/MetaCoins.DAL/
COPY MetaCoins/MetaCoins.Core/MetaCoins.Core.csproj MetaCoins/MetaCoins.Core/

# restore
RUN dotnet restore MetaCoins/MetaCoins.API/MetaCoins.API.csproj

# copy source code
COPY MetaCoins/MetaCoins.API/ MetaCoins/MetaCoins.API/
COPY MetaCoins/MetaCoins.BLL/ MetaCoins/MetaCoins.BLL/
COPY MetaCoins/MetaCoins.DAL/ MetaCoins/MetaCoins.DAL/
COPY MetaCoins/MetaCoins.Core/ MetaCoins/MetaCoins.Core/

# build
WORKDIR /src/MetaCoins/MetaCoins.API
RUN dotnet build -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

ENV ASPNETCORE_URLS=http://+:5001
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5001

ENTRYPOINT ["dotnet", "MetaCoins.API.dll"]