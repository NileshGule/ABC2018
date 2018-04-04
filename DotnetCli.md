# Dotnet commands

## Create solution and project structure

### Create Solution named `ABC2018`

```bash
dotnet new sln --name ABC2018
```

### Create ASP.NET Core MVC Web project named `TechTalksWeb`

```bash
dotnet new razor --output TechTalksWeb
```

### Add `TechTalksWeb` web project to `ABC2018` solution

```bash
dotnet sln add ./TechTalksWeb/TechTalksWeb.csproj
```

### Create ASP.NET Core Web API project named `TechTalksAPI`

```bash
dotnet new webapi --output TechTalksAPI
```

### Add `TechTalksAPI` api project to `ABC2018` solution

```bash
dotnet sln add ./TechTalksAPI/TechTalksAPI.csproj
```

### Create Dot.NET Core exe project named `TechTalksProcessor`

```bash
dotnet new console --output TechTalksProcessor
```

### Add `TechTalksProcessor` api project to `ABC2018` solution

```bash
dotnet sln add ./TechTalksProcessor/TechTalksProcessor.csproj
```

### Add Newtonsoft.Json package

```bash
dotnet add package Newtonsoft.Json
```