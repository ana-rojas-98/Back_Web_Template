# .NET Enterprise API Starter

A reusable ASP.NET Core Web API foundation for authenticated, multi-company enterprise applications.

This project demonstrates a backend architecture for applications where users can belong to one or more companies, authenticate using JWT, select an active company context, and access application features based on roles and permissions.

I built this starter based on common backend patterns I have used while developing enterprise software and data-driven applications.

## Features

* JWT Bearer authentication
* Multi-company user context
* User-to-company assignments
* Role and permission-based access structure
* Hierarchical permission model
* Company context selection
* Entity Framework Core with SQL Server
* Swagger / OpenAPI documentation
* Swagger Bearer authentication support
* Dependency injection
* DTO-based authentication responses
* Protected API endpoints

## Tech Stack

* .NET 8
* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* JWT Bearer Authentication
* Swagger / OpenAPI

## Why I Built This

Many enterprise applications share the same foundational requirements: authentication, user roles, permissions, and access to data within a specific organization or company.

Instead of rebuilding these concepts for every application, I wanted to create a reusable backend foundation that demonstrates how these concerns can be structured in a .NET Web API.

One of the main challenges was managing users who may belong to multiple companies. The authentication flow loads the companies assigned to a user and creates an authenticated user context. When a user selects a company, the API validates the available company context and generates an updated authentication context for the selected company.

## Authentication Flow

1. The user submits an email and password to the authentication endpoint.
2. The API validates the user.
3. The user's role, companies, and permissions are loaded.
4. A JWT access token is generated.
5. If the user belongs to multiple companies, the client can request the list of available companies.
6. The user selects an active company.
7. The API validates the company assignment and updates the authenticated company context.
8. Protected endpoints can use the authenticated user and company context.

```text
Client
   |
   | Login
   v
Auth API
   |
   | Validate User
   v
User + Role + Companies + Permissions
   |
   | Generate JWT
   v
Authenticated User
   |
   | Select Company
   v
Company Context Validation
   |
   | Updated Access Context
   v
Protected API Resources
```

## Project Structure

```text
Controllers/
    API endpoints and HTTP request handling

Logic/
    Authentication and application logic

Models/
    Entity Framework entities and database context

Models/DTO/
    API request and response models

Utilities/
    Helpers for extracting authenticated user context

Program.cs
    Dependency injection, authentication, CORS, and API configuration
```

## Main API Endpoints

### Authentication

```http
POST /api/Auth
```

Authenticates a user and returns an access token with the user's authentication context.

### Get User Companies

```http
GET /api/Auth/Get-User-Companies
```

Returns the companies assigned to the authenticated user.

Requires JWT authentication.

### Select Company

```http
GET /api/Auth/Get-token-Company/{companyId}
```

Validates that the authenticated user has access to the selected company and generates an updated authentication context.

Requires JWT authentication.

## Configuration

The repository includes an `appsettings.example.json` file with the required configuration structure.

```json
{
  "ApiAuth": {
    "Issuer": "https://localhost:7278/",
    "Audience": "EnterpriseApiStarter",
    "SecretKey": "YOUR_JWT_SECRET_KEY_32_CHARS_MINIMUM"
  },
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_SQL_SERVER_CONNECTION_STRING"
  }
}
```

The default `appsettings.json` does not include secrets. Configure `ApiAuth:SecretKey` with user secrets before running the API locally.

For local development, real secrets should be configured outside the repository.

Initialize .NET User Secrets:

```bash
dotnet user-secrets init
```

Configure the JWT secret:

```bash
dotnet user-secrets set "ApiAuth:SecretKey" "YOUR_DEVELOPMENT_SECRET_32_CHARS_MINIMUM"
```

Configure the database connection:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING"
```

The API will start without a database connection so you can inspect Swagger and verify the server health. Endpoints that query SQL Server, including authentication, require `ConnectionStrings:DefaultConnection`.

## Local Database

This template includes an initial EF Core migration with the minimum tables required by the existing controllers:

* `administration.Companies`
* `security.Users`
* `security.Roles`
* `security.Permissions`
* `security.CompanyUsers`
* `security.RolePermissions`

If you previously applied an older version of this starter migration, recreate the local template database before running `dotnet ef database update` again.

Start a local SQL Server container:

```bash
docker compose up -d
```

Configure the local connection string:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=EnterpriseApiStarterDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;"
```

Apply the migration:

```bash
dotnet ef database update
```

Seed credentials for local testing:

```text
Email: admin@example.com
Password: Admin123*
```

## Run the Project With Local Database

Clone the repository:

```bash
git clone https://github.com/ana-rojas-98/Back_Web_Template.git
```

Navigate to the project:

```bash
cd Back_Web_Template
```

Restore dependencies:

```bash
dotnet restore
```

Initialize user secrets:

```bash
dotnet user-secrets init
```

Configure the JWT secret:

```bash
dotnet user-secrets set "ApiAuth:SecretKey" "CHANGE_THIS_LOCAL_SECRET_KEY_32_CHARS"
```

Start SQL Server locally with Docker:

```bash
docker compose up -d
```

Configure the local database connection:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=EnterpriseApiStarterDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;"
```


Apply the EF Core migration:

```bash
dotnet ef database update
```

Run the API:

```bash
dotnet run
```

Test the local seed user:

```text
Email: admin@example.com
Password: Admin123*
```

Verify the API is running:

```text
http://localhost:5215/health
```

Swagger is available in the development environment at:

```text
https://localhost:5215/swagger
```

## Authentication with Swagger

1. Authenticate using the login endpoint.
2. Copy the returned JWT access token.
3. Click **Authorize** in Swagger.
4. Enter:

```text
Bearer YOUR_TOKEN
```

5. Execute protected API endpoints.

## Current Scope

This repository focuses on demonstrating the authentication, company context, and permission structure of an enterprise API.

The project is being progressively refactored into a fully reusable starter by separating domain-specific logic from the core authentication and authorization foundation.

## Roadmap

* Secure password hashing and verification
* Policy-based permission authorization
* Standardized HTTP error responses with Problem Details
* Configurable JWT expiration
* Database migrations and demo seed data
* Integration and unit tests
* GitHub Actions CI pipeline
* Configurable CORS origins
* Docker support
* Companion Angular enterprise starter

## Companion Frontend

A companion Angular starter is also being developed to demonstrate the complete authentication and company-selection flow from the frontend.

The frontend foundation includes:

* Angular 18
* JWT authentication flow
* Protected routes
* Authentication guards
* Permission-aware application structure
* Enterprise dashboard foundations


