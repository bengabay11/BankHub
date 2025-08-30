# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the solution and project files
COPY *.sln .
COPY BL/*.csproj BL/
COPY Dal/*.csproj Dal/
COPY WebAPI/*.csproj WebAPI/

# Restore NuGet packages
RUN dotnet restore

# Copy the source code
COPY BL/. BL/
COPY Dal/. Dal/
COPY WebAPI/. WebAPI/

# Build the application
RUN dotnet publish WebAPI/WebAPI.csproj -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /src
COPY . .
WORKDIR /app
COPY --from=build /app/out .

# Install netcat for database connection check
RUN apt-get update && apt-get install -y netcat-traditional && rm -rf /var/lib/apt/lists/*

# Create and set up the entrypoint script
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 80

ENTRYPOINT ["/app/entrypoint.sh"]
