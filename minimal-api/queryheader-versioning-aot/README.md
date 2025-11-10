# Query/Header Versioning with Native AOT

This project demonstrates ASP.NET Core API versioning using query string and header-based versioning with Native AOT compilation support.

## Features

- **.NET 10 RC2** with Native AOT (`PublishAot=true`)
- **Query String Versioning**: `?api-version=1.0` or `?api-version=2.0`
- **Header Versioning**: `X-Api-Version: 1.0` or `X-Api-Version: 2.0`
- **OpenAPI Documentation**: Separate endpoints for v1 and v2 (`/openapi/v1.json`, `/openapi/v2.json`)
- **Asp.Versioning** packages (v8.1.0)
- **AOT-Compatible JSON Serialization** using `JsonSerializerContext`

## API Endpoints

### Users API
- **GET** `/api/users` - Returns users (v1: basic info, v2: includes birth date)
- **DELETE** `/api/users/{id}` - Version-neutral endpoint to delete a user

### Scores API
- **GET** `/api/scores` - Returns scores (v1: basic score, v2: includes achievement timestamp)

## Usage Examples

### Query String Versioning
```bash
# Get users v1
curl "http://localhost:5000/api/users?api-version=1.0"

# Get users v2
curl "http://localhost:5000/api/users?api-version=2.0"
```

### Header Versioning
```bash
# Get users v1
curl -H "X-Api-Version: 1.0" "http://localhost:5000/api/users"

# Get users v2
curl -H "X-Api-Version: 2.0" "http://localhost:5000/api/users"
```

## Building and Running

### Development
```bash
dotnet run
```

### AOT Publish
```bash
dotnet publish -c Release
```

The native binary will be created at `bin/Release/net10.0/{rid}/publish/queryheader-versioning`

## AOT Compatibility Notes

The project successfully compiles with Native AOT. However, the `Asp.Versioning.Mvc.ApiExplorer` package generates trim analysis warnings during AOT compilation. This is expected behavior as the package contains dynamic model binding code that uses reflection.

These warnings do not prevent the application from running correctly, but indicate areas where runtime behavior might differ in AOT-compiled scenarios versus traditional JIT compilation.

## Purpose

This project serves as a test to verify that ASP.NET Core API versioning (Asp.Versioning) is compatible with Native AOT compilation in .NET 10.
