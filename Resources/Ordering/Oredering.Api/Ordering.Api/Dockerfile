#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Resources/Ordering/Oredering.Api/Ordering.Api/Ordering.Api.csproj", "Resources/Ordering/Oredering.Api/Ordering.Api/"]
COPY ["Resources/Ordering/Ordering.Application/Ordering.Application.csproj", "Resources/Ordering/Ordering.Application/"]
COPY ["Resources/Ordering/Ordering.Domain/Ordering.Domain.csproj", "Resources/Ordering/Ordering.Domain/"]
COPY ["Resources/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Resources/Ordering/Ordering.Infrastructure/"]
RUN dotnet restore "Resources/Ordering/Oredering.Api/Ordering.Api/Ordering.Api.csproj"
COPY . .
WORKDIR "/src/Resources/Ordering/Oredering.Api/Ordering.Api"
RUN dotnet build "Ordering.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.Api.dll"]