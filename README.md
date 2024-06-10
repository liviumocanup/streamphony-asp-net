# Streamphony ASP.NET API

## Abstract

This project adheres to clean architecture principle and employs GraphQL for efficient data queries.
Developed with [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet) for an internship project.

## Prerequisites
1. .NET SDK: The project requires the .NET 5.0 SDK or later. Check your installed version with `dotnet --version`. If you need to install it or update it, you can download the SDK from [the official .NET download page](https://dotnet.microsoft.com/download).

2. **Docker**: Since the project uses SQL Server running in a Docker container, Docker Desktop must be installed on your machine. Docker is used to create, manage, and run containers on Windows, macOS, and Linux. Download Docker Desktop from [the official Docker website](https://www.docker.com/).

3. **Entity Framework Core CLI**: The EF Core CLI is used for database migrations. It's included with the .NET SDK, but you can ensure it's installed with the command `dotnet ef`.

## Configuration

1. Set up the SQL Server with Docker, by pulling the image and starting the container with the following commands:
```bash
docker pull mcr.microsoft.com/mssql/server

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong(!)Password" -p 1433:1433 --name sqlserver -h sqlserver -d mcr.microsoft.com/mssql/server
```

* `mcr.microsoft.com/mssql/server`: Specifies the Docker image to use.
* `-e "ACCEPT_EULA=Y"`: Accepting the End User License Agreement for SQL Server (required to run the container).
* `-e "SA_PASSWORD=YourStrong(!)Password"`: Sets an environment variable for the SA (System Administrator) account password of SQL Server. Replace `YourStrong(!)Password` with a strong password.
* `-p 1433:1433`: Maps port 1433 of the container to port 1433 on the host machine. 
* `--name sqlserver -h sqlserver`: Assigns the name `sqlserver` and hostname `sqlserver` to the new container.
* `-d`: Runs the container in detached mode, meaning the container runs in the background.

Update the `appsettings.json` with your data:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=streamphonydb;User Id=sa;Password=YourStrong(!)Password;"
}
```

2. The migrations are already generated, therefore you can update the database only by running the following command from the `Streamphony.WebAPI` folder:
```bash
dotnet ef database update --project ../Streamphony.Infrastructure/Streamphony.Infrastructure.csproj --startup-project .
```

3. Use the `dotnet run` command from the `Streamphony.WebAPI` folder to start the application.

## Migration Commands
You can generate the migrations from `Streamphony.WebAPI` by using the following commands:
```bash
dotnet ef migrations add InitialCreate -o Persistence/Migrations --project ../Streamphony.Infrastructure/Streamphony.Infrastructure.csproj --startup-project .
```

And in order to drop the Database, run:
```bash
dotnet ef database drop --project ../Streamphony.Infrastructure/Streamphony.Infrastructure.csproj --startup-project .
```


## SonarQube Set Up

You will need Java 17 in `PATH` in order to run SonarQube.

1. Start SonarQube in Docker:
```bash
docker run -d --name sonarqube -p 9000:9000 sonarqube
```

* `-d` runs the container in detached mode (in the background).
* `--name sonarqube` assigns the container a name (sonarqube).
* `-p 9000:9000` maps port 9000 of the container to port 9000 on your host, allowing you to access the SonarQube web interface via http://localhost:9000.
* `sonarqube` specifies the image to use. By default, this pulls the latest version of SonarQube from Docker Hub.

2. Install Scanner .NET Core Global Tool
```bash
dotnet tool install --global dotnet-sonarscanner
```

3. Follow the instructions on http://localhost:9000
```bash
dotnet sonarscanner begin /k:"streamphony-layer" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_yourtoken"
dotnet build
dotnet sonarscanner end /d:sonar.token="sqp_yourtoken"
```