#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true 
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV NUGET_PACKAGES=/root/.nuget/fallbackpackages
ENV NUGET_FALLBACK_PACKAGES=/root/.nuget/fallbackpackages
ENV DOTNET_EnableDiagnostics=0

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Pet.Api/Pet.Api.csproj", "Pet.Api/"]
RUN dotnet restore "Pet.Api/Pet.Api.csproj"
COPY src/ .
WORKDIR "/src/Pet.Api"
RUN dotnet build "Pet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pet.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pet.Api.dll"]