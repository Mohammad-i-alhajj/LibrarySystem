# LibrarySystem

**LibrarySystem** is a clean and beginner-friendly ASP.NET Core MVC project for managing authors and books.  
It provides basic CRUD operations for both entities, follows a layered architecture, and uses Entity Framework Core with SQL Server (LocalDB) for easy setup.

---

## Features
- **Manage Authors** – Add, update, delete, and search authors.
- **Manage Books** – Add, update, delete, and search books.
- **Logical Delete** – Marks records as deleted instead of removing them permanently.
- **Layered Architecture** – Clear separation between `Domains`, `Infrastructure`, `Services`, and `UI`.
- **Responsive UI** – Built with Bootstrap for a clean and simple interface.

---

## Technologies Used
- **ASP.NET Core 8 MVC**
- **Entity Framework Core 8**
- **SQL Server (LocalDB)**
- **C#**
- **Bootstrap 5**
- **HTML, CSS, JavaScript**

---

## Quick Start
Follow these steps to run the project locally:

1. **Clone the repository**
    ```bash
    git clone https://github.com/Mohammad-i-alhajj/LibrarySystem/

3. **Open the solution**
    ```bash
    LibrarySystem.sln

5. **Apply database migrations**
    In Package Manager Console (Default project = Infrastructure):
   ```bash
    Update-Database

6. **Run the project**
    ```bash
    Press F5 or click the green Run button in Visual Studio.

---

## Configuration
   Default database connection:

   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=LibrarySystemDb;Trusted_Connection=True;TrustServerCertificate=True"
   }

   You can change this in UI/appsettings.json if you want to use a different SQL Server.

---

## License
   This project is open-source and free to use for learning purposes.

