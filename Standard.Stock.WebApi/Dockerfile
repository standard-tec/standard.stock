#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Standard.Stock.WebApi/Standard.Stock.WebApi.csproj", "Standard.Stock.WebApi/"]
COPY ["Standard.Stock.Application/Standard.Stock.Application.csproj", "Standard.Stock.Application/"]
COPY ["Standard.Stock.Event/Standard.Stock.Event.csproj", "Standard.Stock.Event/"]
COPY ["Standard.Stock.Infrastructure/Standard.Stock.Infrastructure.csproj", "Standard.Stock.Infrastructure/"]
COPY ["Standard.Stock.Domain/Standard.Stock.Domain.csproj", "Standard.Stock.Domain/"]
RUN dotnet restore "Standard.Stock.WebApi/Standard.Stock.WebApi.csproj"
COPY . .
WORKDIR "/src/Standard.Stock.WebApi"
RUN dotnet build "Standard.Stock.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Standard.Stock.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Standard.Stock.WebApi.dll"]
