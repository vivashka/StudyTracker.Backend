FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
COPY . ./
RUN dotnet restore ./StudyTracker.sln
RUN dotnet publish ./StudyTracker.sln -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudyTracker.Host.dll"]
