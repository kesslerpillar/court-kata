FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY CourtKata.sln ./
COPY CourtApi/CourtApi.csproj CourtApi/
RUN dotnet restore /p:RestoreUseSkipNonexistentTargets="false" -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/CourtApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CourtApi.dll"]