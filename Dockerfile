FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# domain layer
COPY ["Task1.Domain/*", "Task1.Domain/"]
RUN dotnet restore "Task1.Domain/Task1.Domain.csproj"

# dataAccess layer
COPY ["Task1.DataAccess/*", "Task1.DataAccess/"]
RUN dotnet restore "Task1.DataAccess/Task1.DataAccess.csproj"

# business layer
COPY ["Task1.Business/*", "Task1.Business/"]
RUN dotnet restore "Task1.Business/Task1.Business.csproj"

# api layer
COPY ["Task1.Api/*", "Task1.Api/"]
RUN dotnet restore "Task1.Api/Task1.Api.csproj"

WORKDIR "/src/Task1.Api/"
RUN dotnet build "Task1.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Task1.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Task1.Api.dll"]