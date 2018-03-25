FROM microsoft/aspnetcore-build AS build-env

WORKDIR /TechTalksAPI

COPY TechTalksAPI.csproj ./
COPY NuGet.config ./
RUN dotnet restore

COPY . ./
RUN dotnet publish --configuration Release --output releaseOutput --no-restore

#build runtime image
FROM microsoft/aspnetcore

WORKDIR /TechTalksAPI

COPY --from=build-env /TechTalksAPI/releaseOutput ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "TechTalksAPI.dll"]