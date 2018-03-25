FROM microsoft/aspnetcore-build AS build-env

WORKDIR /TechTalksWeb

COPY TechTalksWeb.csproj ./
COPY NuGet.config ./
RUN dotnet restore

COPY . ./
RUN dotnet publish --configuration Release --output releaseOutput --no-restore

#build runtime image
FROM microsoft/aspnetcore:2.0

WORKDIR /TechTalksWeb

COPY --from=build-env /TechTalksWeb/releaseOutput ./

EXPOSE 80

ENTRYPOINT ["dotnet", "TechTalksWeb.dll"]