# Real Estate Import Lab (DAL/BLL/Presentation)

This repository contains a reference implementation of a 3-layer server-side architecture with IoC/DI and ORM support.

## Implemented Requirements

1. Layered architecture:
- `RealEstate.DAL`: ORM entities, DbContext, repositories, CSV reader implementation.
- `RealEstate.BLL`: business service for reading denormalized CSV and saving to DB.
- `RealEstate.Presentation`: interfaces only (no logic).

2. Data model:
- `Owner`: id, name, contactInfo.
- `Apartment`: id, address, price, roomCount.
- `Photo`: id, url, description.
- `Review`: id, name, text.
- `Rating`: id, score.
- `Reservation`: id, startDate, endDate, status.

3. CSV requirements:
- Single denormalized CSV file with all fields.
- Separate CLI module to generate `1000+` rows.
- BLL import logic prevents duplicate `Owner` and `Apartment` records.

4. IoC/DI:
- BLL depends on DAL interfaces, not concrete implementations.
- Composition root is in CLI importer project using dependency injection.

## Project Structure

```
src/
	RealEstate.Domain/
	RealEstate.DAL/
	RealEstate.BLL/
	RealEstate.Presentation/
	RealEstate.Importer.Cli/
	RealEstate.CsvGenerator.Cli/
```

## Prerequisites

- .NET SDK 8.0+

## Build

From repository root:

```bash
dotnet build src/RealEstate.Importer.Cli/RealEstate.Importer.Cli.csproj
dotnet build src/RealEstate.CsvGenerator.Cli/RealEstate.CsvGenerator.Cli.csproj
```

## Generate CSV (1000+ rows)

```bash
dotnet run --project src/RealEstate.CsvGenerator.Cli/RealEstate.CsvGenerator.Cli.csproj -- --output data/import.csv --rows 1200
```

## Import CSV into Database

```bash
dotnet run --project src/RealEstate.Importer.Cli/RealEstate.Importer.Cli.csproj -- --csv data/import.csv --db Data Source=realestate.db
```

The importer automatically creates the database schema with EF Core via `EnsureCreated()`.

## Inspect Database Contents (Verification)

```bash
dotnet run --project src/RealEstate.DbInspector.Cli/RealEstate.DbInspector.Cli.csproj -- --db Data Source=realestate.db
```

Shows table row counts and sample data from the imported database.
