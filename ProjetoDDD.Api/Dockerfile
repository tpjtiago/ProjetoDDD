FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjetoDDD.Api/ProjetoDDD.Api.csproj", "ProjetoDDD.Api/"]
COPY ["Crosscutting/", "Crosscutting/"]
COPY ["ProjetoDDD.IoC/ProjetoDDD.IoC.csproj", "ProjetoDDD.IoC/"]
COPY ["ProjetoDDD.Application/ProjetoDDD.Application.csproj", "ProjetoDDD.Application/"]
COPY ["ProjetoDDD.Domain/ProjetoDDD.Domain.csproj", "ProjetoDDD.Domain/"]
COPY ["ProjetoDDD.Infrastructure/ProjetoDDD.Infrastructure.csproj", "ProjetoDDD.Infrastructure/"]
RUN dotnet restore "ProjetoDDD.Api/ProjetoDDD.Api.csproj"
COPY . .
WORKDIR "/src/ProjetoDDD.Api"
RUN dotnet build "ProjetoDDD.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjetoDDD.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjetoDDD.Api.dll"]
