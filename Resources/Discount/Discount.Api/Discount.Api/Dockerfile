#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Resources/Discount/Discount.Api/Discount.Api/Discount.Api.csproj", "Resources/Discount/Discount.Api/Discount.Api/"]
RUN dotnet restore "Resources/Discount/Discount.Api/Discount.Api/Discount.Api.csproj"
COPY . .
WORKDIR "/src/Resources/Discount/Discount.Api/Discount.Api"
RUN dotnet build "Discount.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Api.dll"]