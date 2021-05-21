#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ParkingService.csproj", "ParkingService/"]
RUN dotnet restore "ParkingService/ParkingService.csproj"

WORKDIR "/src/ParkingService"
COPY . .
RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;
RUN dotnet build "ParkingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParkingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParkingService.dll"]