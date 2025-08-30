#!/bin/bash

# Wait for PostgreSQL to be ready
echo "Waiting for PostgreSQL..."
until nc -z db 5432; do
  echo "PostgreSQL is unavailable - sleeping"
  sleep 1
done
echo "PostgreSQL is up - running migrations"

# Apply migrations and seed data
dotnet tool install --global dotnet-ef
export PATH="$PATH:/root/.dotnet/tools"
cd /src
dotnet ef database update --project Dal/Dal.csproj --startup-project WebAPI/WebAPI.csproj

cd /app
echo "Starting application..."
exec dotnet WebAPI.dll
