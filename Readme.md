# PCStore — Modular E‑Commerce API

**PCStore** is a robust E-Commerce backend API built with **.NET 9** and **ASP.NET Core**. It demonstrates advanced software architecture principles, specifically **Onion Architecture** and the **CQRS** pattern, making it a scalable solution for managing complex e-commerce operations.

## 🚀 Key Features

* **Clean Architecture:** Implements Onion Architecture with a clear separation of concerns.
* **CQRS Pattern:** Uses **MediatR** to decouple command and query responsibilities.
* **Product Management:** Full CRUD for Products, Brands, Categories, Attributes, and Images.
* **User Management:** ASP.NET Identity with **JWT Authentication**, Role-based authorization, and Profile management.
* **Commerce Features:** Shopping Cart, Wishlists, Order Management, and Coupon/Discount systems.
* **Social & Interaction:** Commenting system, Q&A, and Product Ratings.
* **Data Seeding:** Integrated **Faker** to generate realistic test data (users, products, reviews) automatically.

## 🛠 Tech Stack

* **Framework:** .NET 9 (ASP.NET Core Web API)
* **Database:** Entity Framework Core (SQL Server)
* **Architecture:** Onion Architecture + CQRS (MediatR)
* **Auth:** ASP.NET Identity + JWT Bearer
* **Mapping:** AutoMapper
* **Documentation:** Swagger / OpenAPI
* **Utilities:** Bogus/Faker (Data Seeding)

## 📂 Project Structure

* **`PCStore.Core`**: Contains the Domain entities, Enums, and Application logic (Interfaces, CQRS Handlers, DTOs, Validators).
* **`PCStore.Persistence`**: Handles Database Context, Migrations, Repositories, and Data Seeding.
* **`PCStore.API`**: The entry point (Controllers, DI Container, Middleware configuration).

## ⚙️ Getting Started

### Prerequisites
* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* SQL Server (Local or Docker)
* `dotnet-ef` tool (`dotnet tool install --global dotnet-ef`)

### Configuration
Update `PCStore.API/appsettings.json` with your database connection and a secure JWT key.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=PCStoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Issuer": "PCStore",
    "Audience": "PCStoreUsers",
    "Key": "YOUR_SUPER_SECRET_LONG_KEY_HERE"
  },
  "SmtpSettings": {
    "Host": "smtp.example.com",
    "Port": 587,
    "UserName": "user",
    "Password": "password"
  }
}
```

### Installation & Running

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/Zeth21/PCStore.git](https://github.com/Zeth21/PCStore.git)
    cd PCStore
    ```

2.  **Apply Migrations & Database Update:**
    ```bash
    dotnet ef database update -p PCStore.Persistence -s PCStore.API --context ProjectDbContext
    ```

3.  **Run the API:**
    ```bash
    cd PCStore.API
    dotnet run
    ```

4.  **Access Swagger:**
    The API documentation will be available at `https://localhost:5001/` (or your configured port).

## 🧪 Test Data (Seeding)
On the first run, the application automatically seeds the database with sample data.
* **Default Password for Seeded Users:** `Test1234!`

## 🤝 Contributing
Contributions are welcome! Please fork the repository and create a Pull Request for any features or bug fixes.

## 📄 License
This project is open-source. Feel free to use it for educational purposes.

## Zeyitcan DASDEMIR © 2025