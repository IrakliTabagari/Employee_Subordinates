# Engini.Employees

## Prerequisites
- .NET SDK
- SQL Server (localhost:1433)
- Database: `Employees`
- SQL User: `sa` / Password: `Secret123`

## Setup Database
```bash
dotnet ef database update --project Engini.Employees.Api
```

## Run
```bash
dotnet run --project Engini.Employees.Api
```

## API Call
```bash
GET http://localhost:5202/Employee/{id}
```

### Example
```bash
curl http://localhost:5202/Employee/1
```
