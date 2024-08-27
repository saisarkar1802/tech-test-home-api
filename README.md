# Tech Test Home API

## Overview
This project is a simple CRUD API designed for managing house units. It is built using C# and .NET, providing endpoints for creating, reading, updating, and deleting house unit records. Authentication is handled using Auth0.

## Features
- **Create House Unit**: Add new house unit records.
- **Read House Units**: Fetch details of existing house units.
- **Update House Unit**: Modify details of a specific house unit.
- **Delete House Unit**: Remove a house unit from the database.
- **Authentication**: Secure API access using Auth0.

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/saisarkar1802/tech-test-home-api.git
    ```
2. Navigate to the project directory:
    ```bash
    cd tech-test-home-api/HouseUnitAPI
    ```
3. Restore dependencies:
    ```bash
    dotnet restore
    ```
4. Build the project:
    ```bash
    dotnet build
    ```

## Auth0 Setup

1. Create an Auth0 account at [Auth0](https://auth0.com/).
2. Create a new application in your Auth0 dashboard.
3. Update the `appsettings.json` file with your Auth0 domain and audience.

## Running the API
To run the API locally, use the following command:
```bash
dotnet run
...

The API will be accessible at 'http://localhost:5000'.

## API Endpoints
- **GET /api/houseunits**: Retrieve all house units.
- **GET /api/houseunits/{id}**: Retrieve a specific house unit by ID.
- **POST /api/houseunits**: Create a new house unit.
- **PUT /api/houseunits/{id}**: Update an existing house unit.
- **DELETE /api/houseunits/{id}**: Delete a house unit by ID.

## Technologies & Frameworks Used
- **C#**
- **.NET 8**
- **SQL**
