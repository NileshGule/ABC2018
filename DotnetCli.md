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

### Create class library project named `TechTalksModel`

```bash
dotnet new classlib --output TechTalksModel
```

### Add `TechTalksModel` api project to `ABC2018` solution

```bash
dotnet sln add ./TechTalksModel/TechTalksModel.csproj
```

### Add `TechTalksMode` project reference to `TechTalksAPI` project

```bash
dotnet add reference ../TechTalksModel/TechTalksModel.csproj
```

### Add `Automapper` to the project

```bash
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```

### Create Dot.NET Core exe project named `TechTalksELKProcessor`

```bash
dotnet new console --output TechTalksELKProcessor
```

### Add `TechTalksELKProcessor` api project to `ABC2018` solution

```bash
dotnet sln add ./TechTalksELKProcessor/TechTalksELKProcessor.csproj
```

### Add `NEST` package to the project `TechTalksELKProcessor`

```bash
dotnet add package NEST
```