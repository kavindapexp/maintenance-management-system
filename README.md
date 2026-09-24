# Maintenance Management System

A full-stack maintenance management application built as a DevOps and CI/CD portfolio project.

The application manages equipment, sites, companies, maintenance requests, and basic asset-finance information through a React frontend and ASP.NET Core Web API backed by PostgreSQL.

## Architecture

```text
React Frontend
     |
     | REST / JSON
     v
ASP.NET Core Web API
     |
     | Entity Framework Core
     v
PostgreSQL
```

## Technology Stack

### Frontend

- React
- Vite
- JavaScript
- HTML/CSS

### Backend

- ASP.NET Core Web API
- C#
- Entity Framework Core
- REST APIs

### Database

- PostgreSQL 17
- Entity Framework Core migrations

### DevOps Roadmap

- Git / GitHub
- Docker
- Docker Compose
- GitHub Actions
- Docker Hub
- Kubernetes
- Monitoring and observability

## Current Features

### Equipment Management

- View equipment
- Create equipment
- Edit equipment
- Delete equipment
- Associate equipment with sites
- Track equipment status

### Maintenance Requests

- Create maintenance requests
- View maintenance requests
- Edit maintenance requests
- Delete maintenance requests
- Associate requests with equipment
- Track priority and status

### Business Data

- Countries
- Companies
- Sites
- Currencies
- Asset values
- Depreciation
- Asset valuations
- Inflation indexes

## Project Structure

```text
maintenance-management-system/
|-- frontend/
|   `-- React application
|
|-- src/
|   `-- MaintenanceManagement.Api/
|       |-- Controllers/
|       |-- Data/
|       |-- Dtos/
|       |-- Migrations/
|       |-- Models/
|       |-- Program.cs
|       `-- appsettings.example.json
|
`-- README.md
```

## Running Locally

### Prerequisites

- .NET SDK
- Node.js
- Docker Desktop

### Start PostgreSQL

The development database runs in a PostgreSQL Docker container.

### Start the API

```powershell
cd src/MaintenanceManagement.Api
dotnet run
```

The API runs on:

```text
http://localhost:5109
```

### Start the frontend

```powershell
cd frontend
npm.cmd install
npm.cmd run dev
```

The frontend runs on:

```text
http://localhost:5173
```

## Database Configuration

Copy the example configuration and provide your local PostgreSQL credentials.

Do not commit real database credentials to source control.

## Development Status

The current application provides a working full-stack CRUD implementation with React, ASP.NET Core, Entity Framework Core, and PostgreSQL.

The project is being progressively extended into a production-style CI/CD portfolio demonstrating containerization, automated builds, Kubernetes deployment, troubleshooting, and observability.
