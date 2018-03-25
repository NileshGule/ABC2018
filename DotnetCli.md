# Dotnet commands

## Create solution and project structure

### Create Solution named `ABC2018`

```bash
dotnet new sln --name ABC2018
```

### Add MVC Web app named `TechTalksWeb`

```bash
dotnet new razor --output TechTalksWeb
```

### Add MVC Web `TechTalksWeb` to `ABC2018` solution

```bash
dotnet sln add ./TechTalksWeb/TechTalksWeb.csproj
```