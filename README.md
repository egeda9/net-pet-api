# PETS API (Net 7)

# REST API Microservice

This is the PETS REST API microservice documentation which enables the administration of CRUD operations for pets data management.

It uses [.NET 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) as base language and a [docker](https://www.docker.com) container-based deployment scheme

# Continuous Integration process

[Continuous Integration process (Branching patterns)](https://martinfowler.com/articles/branching-patterns.html)

## Prerequisites to run it locally

    - Docker
    - Visual Studio Code or Visual Studio 2022 Community Edition
    - .NET Core 7.0
    
# Quick Start

After git clone, execute commands below in root directory:

```

+---Pet.Api                        //web api
|   +---Controllers                //-- controllers folder
|   +---Program.cs                 //-- startup class and DI definition
|   +---Pet.Api.csproj             //-- project file
|   \---appsettings.json           //-- settings file
+---Pet.Api.Model                  //model class libraryr
|   +---Enum                       //-- enums folder
|   +---FileStorageConfig.cs       //-- feature toggle config class
|   \---Pet.Api.Model.csproj       //-- project file
+---Pet.Api.Repository             //model class library
|   +---Implementation             //-- implementation classes folder
|   +---Interfaces                 //-- interfaces folder
|   \---Pet.Api.Repository.csproj  //-- project file 
\---Pet.Api.Util                   //util class library
|   +---Implementation             //-- implementation classes folder
|   +---Interfaces                 //-- interfaces folder
|   \---Pet.Api.Util.csproj        //-- project file
\---Pet.Api.Service                //service class library
|   +---Implementation             //-- implementation classes folder
|   +---Interfaces                 //-- interfaces folder
|   \---Pet.Api.Service.csproj     //-- project file 
```
## Install

    dotnet restore "Pet.Api/Pet.Api.csproj"

## Run the app

    dotnet build "Pet.Api.csproj"
    dotnet run --project Pet.Api.csproj

## Accessing the API (From a local docker deployment)

- http://localhost:50420/swagger/index.html

### Docker known issues

1. _Grpc.Core.Internal.UnmanagedLibrary Attempting to load native library `/app/libgrpc_csharp_ext.x64.so`

- **Solution**: Use a non-alpine image that should have built the `libgrpc_csharp_ext` releases. **Alternative**: Build from source code and install. 

# Open API definition

The REST API app is described below.

[Open API Definition](./src/Pet.Api/swagger.json)

## How to use it?

1. Class member property creation.

```cs
private readonly IStorageHandler _storageHandler;
```

2. Service Injection

```cs
/// <summary>
/// 
/// </summary>
/// <param name="storageHandler"></param>
public MyControllerController(IStorageHandler storageHandler)
{
	this._storageHandler = storageHandler;
}
```

3. Usage

```cs
var doc = await this._storageHandler.ReadAsync();
```

## appsettings.json 

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "FileStorageConfig": {
    "FilePath": "pets.json"
  }
}

```

## [infra](infra/) folder and env settings

- Deployment definition (per environment)
- Environment variables definition (per environment)
