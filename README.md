# Author : Melisa Demir
# Inventory Management System

A simple Inventory Management System built with ASP.NET Core 8 to demonstrate modern backend development concepts.

## Features

- JWT Authentication
- Role-Based Authorization (Admin/User)
- Product CRUD Operations
- Repository Pattern
- Service Layer Architecture
- FluentValidation
- Global Exception Middleware
- SQL Server with Entity Framework Core
- Redis Cache (IDistributedCache)
- Cache-Aside Pattern
- Cache Invalidation
- YARP API Gateway
- Swagger Authentication Support

## Technologies

- ASP.NET Core 8
- C#
- Entity Framework Core
- SQL Server
- Redis
- YARP Reverse Proxy
- JWT Authentication
- FluentValidation
- Swagger / OpenAPI

## Project Structure

```
InventoryManagementSystem
│
├── ApiGateway
├── AuthService
└── InventoryService
```

## Architecture

```
Client
   │
   ▼
YARP API Gateway
   │
   ├──────────────┐
   ▼              ▼
AuthService   InventoryService
                    │
                    ▼
              Service Layer
                    │
                    ▼
             Repository Layer
                    │
                    ▼
               SQL Server
                    │
                    ▼
                  Redis
```

# Inventory Management System

A simple Inventory Management System built with ASP.NET Core 8 to demonstrate modern backend development concepts.

## Features

- JWT Authentication
- Role-Based Authorization (Admin/User)
- Product CRUD Operations
- Repository Pattern
- Service Layer Architecture
- FluentValidation
- Global Exception Middleware
- SQL Server with Entity Framework Core
- Redis Cache (IDistributedCache)
- Cache-Aside Pattern
- Cache Invalidation
- YARP API Gateway
- Swagger Authentication Support

## Technologies

- ASP.NET Core 8
- C#
- Entity Framework Core
- SQL Server
- Redis
- YARP Reverse Proxy
- JWT Authentication
- FluentValidation
- Swagger / OpenAPI

## Project Structure

```
InventoryManagementSystem
│
├── ApiGateway
├── AuthService
└── InventoryService
```

## Architecture

```
Client
   │
   ▼
YARP API Gateway
   │
   ├──────────────┐
   ▼              ▼
AuthService   InventoryService
                    │
                    ▼
              Service Layer
                    │
                    ▼
             Repository Layer
                    │
                    ▼
               SQL Server
                    │
                    ▼
                  Redis
```

## What I Learned

During this project I practiced:

- JWT Authentication
- Role-Based Authorization
- Layered Architecture
- Repository Pattern
- Redis Caching
- Cache-Aside Pattern
- Cache Invalidation
- API Gateway with YARP
- Global Exception Handling
- FluentValidation