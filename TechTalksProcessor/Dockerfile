FROM microsoft/aspnetcore-build AS build-env

WORKDIR /TechTalksProcessor

COPY TechTalksProcessor.csproj ./
COPY NuGet.config ./
RUN dotnet restore

COPY . ./
RUN dotnet publish --configuration Release --output releaseOutput --no-restore

#build runtime image
FROM microsoft/aspnetcore

WORKDIR /TechTalksProcessor

COPY --from=build-env /TechTalksProcessor/releaseOutput ./

ENTRYPOINT ["dotnet", "TechTalksProcessor.dll"]