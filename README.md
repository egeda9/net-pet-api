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

### Docker deployment

#### Building the image


```
DOCKER_BUILDKIT=1 docker build -t net-pets-api -f Dockerfile .
```

This will build the image from the Dockerfile in the **/src** directory—the period at the end—and tag it with net-pets-api:latest, with net-pets-api being the image name and latest being the tag name.

#### Create and run the container

```
docker run -d -p 7991:80 --name net-pets-api net-pets-api:latest
```

- **-d**: This is short for detach and means that the Docker container will run in the background.
- **-p 7991:80**: This publishes the port 80 of the container as the port 7991 on the host. Thanks to that, the API will be available on the host at http://localhost:7991/swagger.
- **–name net-pets-api**: The container will be available under the name net-pets-api. 
- **net-pets-api**: This is the name of the image we want to use to create the container.

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

### gcloud - cloud run deployment

#### Deploy to Cloud Run from source

- In the source code directory (/src), deploy from source.

```
gcloud run deploy --source . --update-env-vars ENV=Development,STORAGE_FILE_PATH=./pets.json
```

- Specify service name
- Enable **Artifact Registry API**
- Select region **us-east1**
- Allow unauthenticated invocations

# REST API

The REST API to the pets api is described below.

## Get list of Pets

### Request

`GET /api/pet`

    curl -X 'GET' \
    '{host}/api/pet' \
    -H 'accept: application/json'

### Response

``` json
{
  "data": [
    {
      "id": "a9dbb813-8ac8-4365-8acb-923878b7b437",
      "name": "Lupe",
      "createdAt": "2023-01-27T11:44:02.0268285-05:00",
      "updatedAt": "2023-01-27T16:43:17.101Z",
      "specie": "Dog",
      "sex": "Female",
      "breed": "Poodle",
      "age": 1
    },
    {
      "id": "df78a87b-2a21-4480-aa43-d90dd85ff8c9",
      "name": "Canela",
      "createdAt": "2023-01-27T11:44:31.5572417-05:00",
      "updatedAt": "2023-01-27T16:43:17.101Z",
      "specie": "Dog",
      "sex": "Female",
      "breed": "Labrador Retriever",
      "age": 1.9
    }
  ]
}
```

## Get a specific Pet

### Request

`GET /api/pet/:id`

  curl -X 'GET' \
  '{host}/api/pet/df78a87b-2a21-4480-aa43-d90dd85ff8c9' \
  -H 'accept: application/json'

### Response

``` json
{
  "data": {
    "id": "df78a87b-2a21-4480-aa43-d90dd85ff8c9",
    "name": "Canela",
    "createdAt": "2023-01-27T11:44:31.5572417-05:00",
    "updatedAt": "2023-01-27T16:43:17.101Z",
    "specie": "Dog",
    "sex": "Female",
    "breed": "Labrador Retriever",
    "age": 1.9
  }
}
```

## Create a new Pet

### Request

`POST /api/pet`

    curl -X 'POST' \
	'{host}/api/pet' \
	-H 'accept: application/json' \
	-H 'Content-Type: application/json' \
	-d '{
	"age": 5,
	"breed": "German Shepherd",
	"name": "Riley",
	"sex": "Male",
	"specie": "Dog"
}'

### Response

``` json
{
  "data": {
    "id": "d9d7fe36-2892-483b-848a-641ba9a9a600",
    "name": "Riley",
    "createdAt": "2023-02-01T16:31:46Z",
    "updatedAt": "",
    "specie": "Dog",
    "sex": "Male",
    "breed": "German Shepherd",
    "age": 5
  }
}
```

## Update a Pets's property

### Request

`PUT /api/pet/:id`

  curl -X 'PUT' \
  '{host}/api/pet/d9d7fe36-2892-483b-848a-641ba9a9a600' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "age": 6,
  "breed": "German Shepherd",
  "name": "Riley",
  "sex": "Male",
  "specie": "Dog"
}'

### Response

``` json
{
  "data": {
    "id": "d9d7fe36-2892-483b-848a-641ba9a9a600",
    "name": "Riley",
    "createdAt": "2023-02-01T16:31:46Z",
    "updatedAt": "2023-02-01T16:38:43Z",
    "specie": "Dog",
    "sex": "Male",
    "breed": "German Shepherd",
    "age": 6
  }
}
```

## Delete a Pet

### Request

`DELETE /api/pet/:id`

  curl -X 'DELETE' \
  '{host}/api/pet/d9d7fe36-2892-483b-848a-641ba9a9a600' \
  -H 'accept: application/json'

### Response

``` json
{
  "data": "OK"
}
```