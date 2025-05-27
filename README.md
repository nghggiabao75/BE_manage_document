# Manage Document API

## Prerequisites
- .NET 8.0 SDK
- MySQL Server

## Backend Setup
1. Clone the repository
2. Navigate to the project directory:
```bash
cd ManageDocument/ManageDocument
```
3. Update the database connection string in `appsettings.json` if needed
4. Run the application:
```bash
dotnet run
```
The API will be available at:
- http://localhost:5203
- https://localhost:7150

## Frontend Setup
1. Navigate to the frontend directory
2. Install dependencies:
```bash
npm install
```
3. Run the development server:
```bash
npm run dev
```
The frontend will be available at:
- http://localhost:5173

## Database Setup
1. Create a MySQL database named `managedocument`
2. Run the database migrations:
```bash
dotnet ef database update
```
3. Run the database triggers script located in:
```
DatabaseScript/DatabaseTrigger.txt
```