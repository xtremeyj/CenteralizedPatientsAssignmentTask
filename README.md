# Centralized Patients Assignment Task

This project is a centralized system for managing and assigning patients, built with:

- .NET Core Web API for backend
- Angular for frontend

## Setup Instructions

1. Run Database Script

Execute the SQL script located at:
CenteralizedPatientsAssignmentTask.API/Script/CenteralizedPatientsAssignmentTaskScript.sql

You can run this using SQL Server Management Studio or any compatible SQL tool.

2. Frontend Setup

Navigate to the frontend directory and install dependencies:

cd CenteralizedPatientsAssignmentTask.FrontEnd
npm install

3. Update API URLs in Angular Services

Update the API_URL in the following services:

- AuthService
- AnalyticsService
- PatientService

Example for AuthService:
private readonly API_URL = 'http://localhost:5277/api/Auth/login';

Make sure to replace this with your actual backend API URL if it's hosted differently.

4. Run Angular Frontend

To start the Angular application, run:

ng serve

The app will be accessible at: http://localhost:4200

5. Backend Configuration

Open CenteralizedPatientsAssignmentTask.API/appsettings.json and update the connection string:

"ConnectionStrings": {
  "DefaultConnection": "Your-SQL-Server-Connection-String"
}

Run the backend using Visual Studio or via CLI:

dotnet run

The API should run on http://localhost:5277 by default.

## Summary

- Run SQL script to initialize DB
- Update API URLs in Angular services
- Update backend DB connection string
- Run frontend with ng serve
- Run backend with dotnet run

## Notes

- Ensure SQL Server is running before starting the backend
- Adjust CORS settings in the API if running frontend and backend on different ports
- You can configure user roles and permissions based on Admin, Viewer, etc.
