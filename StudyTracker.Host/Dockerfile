FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StudyTracker.Backend/StudyTracker.Backend.csproj", "StudyTracker.Backend/"]
RUN dotnet restore "StudyTracker.Backend/StudyTracker.Backend.csproj"
COPY . .
WORKDIR "/src/StudyTracker.Backend"
RUN dotnet build "StudyTracker.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StudyTracker.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudyTracker.Backend.dll"]
