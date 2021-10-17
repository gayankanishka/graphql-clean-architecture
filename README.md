# GraphQL Clean Architecture with Hot Chocolate

![chilli cream log](https://github.com/gayankanishka/graphql-clean-architecture/blob/533a59d7e96493b4d9e94f8fe08c04a4dc3f6af5/docs/assets/ChilliCream.svg?raw=true)

[//]: # (TODO: add description)

> DISCLAIMER: Original non-layered solution could be found at [ChilliCream/graphql-workshop](https://github.com/ChilliCream/graphql-workshop). This repository focuses on applying the clean-architecture into the above solution.

## Database Schema

![database schema](https://github.com/gayankanishka/graphql-clean-architecture/blob/b77a0166917ce6671dc885f4fb3e6ebd1f8bba71/docs/assets/21-conference-planner-db-diagram.png?raw=true)

What's included:

- [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Hot Chocolate](https://chillicream.com/docs/hotchocolate)
- [MediatR](https://github.com/jbogard/MediatR)
- [EF Core](https://docs.microsoft.com/en-us/ef/core/)
- [Npgsql](https://www.npgsql.org/efcore/index.html)

## Table of Content

- [Quick Start](#quick-start)
    - [Prerequisites](#prerequisites)
    - [Development Environment Setup](#development-environment-setup)
    - [Build and run](#build-and-run-from-source)
- [License](#license)

## Quick Start

After setting up your local DEV environment, you can clone this repository and run the solution.

### Prerequisites

You'll need the following tools:

- [.NET](https://dotnet.microsoft.com/download), version `>=5`
- One of the below IDE of your choice
  - [Visual Studio](https://visualstudio.microsoft.com/), version `>=2019`
  - [JetBrains Rider](https://jetbrains.com/rider/), version `>=2020`
  - [Visual Studio Code](https://code.visualstudio.com/)

### Development Environment Setup

First clone this repository locally.

- Install all of the the prerequisite tools mentioned above.

### Build and run from source

With Visual studio:
Open up the solutions using Visual studio.

- Restore solution `nuget` packages.
- Rebuild solution once.
- Run the solution.
- Bana cake pop local URL [here](https://localhost:5001/graphql).
- Voyager local URL [here](https://localhost:5001/graphql-voyager)

## License

Licensed under the [MIT](LICENSE) license.
