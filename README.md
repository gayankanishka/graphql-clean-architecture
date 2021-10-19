# GraphQL Clean Architecture with Hot Chocolate

![chilli cream log](https://github.com/gayankanishka/graphql-clean-architecture/blob/533a59d7e96493b4d9e94f8fe08c04a4dc3f6af5/docs/assets/ChilliCream.svg?raw=true)

The repository contains a backend of a simple Conference Planner. The backend serves as a GraphQL server. You could either use `Sqlite` or `Postgres` to persist the data. Application has docker container orchestration configured.

> DISCLAIMER: Original non-layered solution could be found at [ChilliCream/graphql-workshop](https://github.com/ChilliCream/graphql-workshop). This repository focuses on applying the clean-architecture principles into the above solution. Also this repository contains some additional features.

## Database Schema

![database schema](https://github.com/gayankanishka/graphql-clean-architecture/blob/b77a0166917ce6671dc885f4fb3e6ebd1f8bba71/docs/assets/21-conference-planner-db-diagram.png?raw=true)

What's included:

- [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Hot Chocolate](https://chillicream.com/docs/hotchocolate)
- [MediatR](https://github.com/jbogard/MediatR)
- [EF Core](https://docs.microsoft.com/en-us/ef/core/)
- [SqLight](https://docs.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)
- [Postgres](https://www.npgsql.org/efcore/index.html)
- [Docker](https://docs.docker.com/)

## Table of Content

- [Quick Start](#quick-start)
  - [Prerequisites](#prerequisites)
  - [Development Environment Setup](#development-environment-setup)
  - [Docker Configuration](#docker-configuration)
  - [Database Configuration](#database-configuration)
  - [Database Migrations](#database-migrations)
  - [Build and run](#build-and-run-from-source)
- [License](#license)

## Quick Start

After setting up your local DEV environment, you can clone this repository and run the solution.

### Prerequisites

You'll need the following tools:

- [.NET](https://dotnet.microsoft.com/download), version `>=6`
- One of the below IDE of your choice
  - [Visual Studio](https://visualstudio.microsoft.com/), version `>=2022`
  - [JetBrains Rider](https://jetbrains.com/rider/), version `>=2021.3`
  - [Visual Studio Code](https://code.visualstudio.com/)

### Development Environment Setup

First clone this repository locally.

- Install all of the the prerequisite tools mentioned above.

### Docker Configuration

In order to get Docker working, you will need to add a temporary SSL cert and mount a volume to hold that cert. You can find [Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-6.0) that describe the steps required for Windows, macOS, and Linux.

The following will need to be executed from your terminal to create a cert.

For Windows:

```bash
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p 1qaz2wsx@
dotnet dev-certs https --trust
```

FOR macOS:

```bash
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p 1qaz2wsx@
dotnet dev-certs https --trust
```

FOR Linux:

```bash
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p 1qaz2wsx@
```

In order to build and run the docker containers locally, execute below command from the root of the solution.

```bash
docker-compose -f 'docker-compose.yml' up --build
```

### Database Configuration

The default configuration of the application is to use an `Sqlite` database for persistence.

If you would like to use `Postgres` as the db layer, you will need to update `src/GraphQL/appsettings.json` as follows:

```json
{
  "ConnectionStrings": {
    "PostgresDbConnection": "YOUR_POSTGRES_CONNECTION_STRING"
  },
  "UseSqlite": "false"
}
```

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied. The sample test data in `NDC_London_2019.json` will be imported automatically.

### Database Migrations

To use `dotnet-ef` for your migrations run below commands from the root of the project.

Run Migrations:

```bash
dotnet ef migrations add InitialMigration --project src/Infrastructure --startup-project src/GraphQL --output-dir Persistence/Migrations
```

Update database:

```bash
dotnet ef database update --project src/Infrastructure --startup-project src/GraphQL
```

### Build and run from source

With Visual studio:
Open up the solutions using Visual studio.

- Restore solution `nuget` packages.
- Rebuild solution once.
- Run the solution.
- Banana cake pop local URL [here](https://localhost:5001/graphql).
- Voyager local URL [here](https://localhost:5001/graphql-voyager)

## License

Licensed under the [MIT](LICENSE) license.

## PRs Welcomed
