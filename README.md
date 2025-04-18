# 🏦 BankHub

**BankHub** is a robust backend application developed using ASP.NET Core. It serves as the core of the BankHub system, providing RESTful APIs for managing users, accounts, transactions, and more.

---

## ✨ Features

- User authentication and authorization
- Account management
- Transaction processing
- Role-based access control
- Comprehensive API documentation with Swagger
- Error handling and logging

---

## 🛠 Technologies

- **Framework**: ASP.NET Core
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Authentication**: ASP.NET Identity
- **API Documentation**: Swagger

---

## 🚀 Getting Started

### Prerequisites

- .NET 6.0 SDK or higher
- PostgreSQL

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/bengabay11/bankhub.git
   cd bankhub
   ```

2. Set up the database:

   ```bash
   dotnet ef database update
   ```

3. Run the application:

   ```bash
   dotnet run
   ```

4. Access the API documentation at [http:/localhost:5000/swagger](http:/localhost:5209/swagger)

## ⚙️ Configuration

The application uses [appsettings.json](WebAPI/appsettings.json) for configuration. Update the file settings as needed.

## 📖 API Documentation

Swagger is integrated for API documentation. Once the application is running, navigate to: [http://localhost:5000/swagger](http://localhost:5000/swagger).
Here, you can explore and test the available endpoints.

## 🗂 Project Structure

The BankHub Server project is organized as follows:​

### 📂 BL/ (Business Logic Layer)

Contains the core business logic of the application, including services, domain models, and interfaces that define the application's operations.​

### 📂 Dal/ (Data Access Layer)

Responsible for data persistence and retrieval. This layer includes Entity Framework Core DbContext, entity configurations, and repository implementations.​

### 📂 WebAPI/

Hosts the ASP.NET Core Web API project. It includes controllers, middleware, and configurations necessary to expose the application's functionalities over HTTP.

## 📄 License

This project is licensed under the MIT License.
