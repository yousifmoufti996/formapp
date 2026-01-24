# FormApp API

A .NET Web API application for managing forms and transactions.

## Features

- Authentication & Authorization
- Transaction Records Management
- Transaction Attachments with Secure Image Handling
- Rate Limiting
- Entity Framework Core with SQL Server
- Clean Architecture (API, Application, Core, Infrastructure layers)

## Tech Stack

- .NET (ASP.NET Core Web API)
- Entity Framework Core
- SQL Server
- FluentValidation
- Serilog for Logging
- Rate Limiting Middleware

## Project Structure

- **FormApp.API** - Web API layer with controllers and middleware
- **FormApp.Application** - Application layer with services, DTOs, and validators
- **FormApp.Core** - Domain layer with entities, enums, and repository interfaces
- **FormApp.Infrastructure** - Data access layer with EF Core and repositories
- **FormApp.Helper** - Utility functions and helpers

## Getting Started

### Prerequisites

- .NET SDK 6.0 or later
- SQL Server

### Setup

1. Clone the repository
```bash
git clone https://github.com/yousifmoufti996/formapp.git
cd formapp
```

2. Update connection string in `appsettings.json`

3. Run migrations
```bash
dotnet ef database update
```

4. Run the application
```bash
dotnet run --project FormApp.API
```

## Configuration

Configure settings in `appsettings.json`:
- Database connection string
- JWT settings
- Rate limit settings
- File upload paths

## License

[Your License Here]
