# 📚 Library Management System (LMS)

A **.NET Core Web API** project for managing a digital library system.  
The system provides features for managing books, members, borrowing/returning, searching, roles, and logging user activities.

---

## 🚀 Features

### 🔹 Book Management
- CRUD operations for books
- Upload & serve book cover images
- Assign multiple authors and categories
- Track book availability status (`Available`, `Borrowed`, `Reserved`)

### 🔹 Member Management
- Manage members registered in the system
- Update/Delete members
- Role-based access (`Member`, `Admin`, `Librarian`)

### 🔹 Borrow Management
- Borrow and return transactions
- Track due dates & return dates
- Prevent borrowing if book is unavailable

### 🔹 Metadata Management
- Manage **Authors**, **Publishers**, and **Categories**
- Support hierarchical categories (e.g., `Fiction > Fantasy`)

### 🔹 Roles & Authentication
- User registration & login with **JWT authentication**
- Role-based access control
- Assign/remove roles (`Admin`, `Librarian`, `Member`)

### 🔹 Search Service
- Search books by:
  - Title
  - Author
  - Category
- Get books by **status** (Available, Borrowed, etc.)

### 🔹 System User Management
- Manage non-member users (Admins, Librarians)
- Update user roles and information

### 🔹 Activity Logging
- Logs all user actions (`Borrow`, `Return`, `Create`, `Delete`, etc.)
- Retrieve activity logs by user

---

## 📂 Project Structure

```
LibraryManagementSystem/
│── Controllers/          # API Controllers
│── Dtos/                 # Data Transfer Objects
│── Helpers/              # JWT & helper configs
│── Migrations/           # EF Core migrations
│── Models/               # Entity models
│── ModelsConfig/         # Entity configurations
│── Services/             # Business logic layer
│   ├── BookService/          # Books (IBookService + BookService)
│   ├── BorrowService/        # Borrow/Return (IBorrowService + BorrowService)
│   ├── MemberService/        # Members (IMemberService + MemberService)
│   ├── MetadataService/      # Authors, Publishers, Categories(ImetadataService,MetadataService)
│   ├── RolesService/         # Role management (IRoleService,RoleService)
│   ├── SearchService/        # (IserachService,SearchService)
│   ├── SystemUserService/    # Book searching (ISystemUserService,SystemUserSrvice)-System user (admins/librarians/staff)
│   └── UserActivityService/  # Logging user actions (IUserServuce,UserService)
│── wwwroot/              # Static files (book images)
│── appsettings.json       # Config (DB, JWT, etc.)
│── Program.cs             # Startup/DI
```

---

## 🛠️ Tech Stack

- **Backend:** ASP.NET Core Web API (.NET 8)
- **ORM:** Entity Framework Core
- **Database:** SQL Server Management System
- **Authentication:** ASP.NET Identity + JWT
- **File Storage:** wwwroot (for book images)

---

## ⚙️ Setup & Run

1. Clone repo:
   ```sh
   git clone https://github.com/yourusername/LibraryManagementSystem.git
   cd LibraryManagementSystem
   ```

2. Update `appsettings.json` with your DB connection and JWT secret.

3. Run migrations:
   ```sh
   dotnet ef database update
   ```

4. Start the project:
   ```sh
   dotnet run
   ```

---

## 📌 Example API Endpoints

### Authentication
- `POST /api/auth/register` → Register user
- `POST /api/auth/login` → Login & get JWT

### Books
- `POST /api/books` → Add new book
- `GET /api/books` → Get all books
- `GET /api/books/{id}` → Get book by ID
- `GET /api/books/status/{status}` → Get books by status
- `PUT /api/books/{id}` → Update book
- `DELETE /api/books/{id}` → Delete book

### Borrowing
- `POST /api/borrow` → Borrow a book
- `PUT /api/borrow/return/{id}` → Return a book

### Search
- `POST /api/search` → Search by Title, Author, Category

### Activity Logs
- `POST /api/activity/log` → Log user activity
- `GET /api/activity/user/{id}` → Get logs for a user

---

## 📸 Notes
- Book cover images are saved in `wwwroot/books/`  
- Returned API includes full URL to serve images

---
## 📊 ERD (Entity Relationship Diagram)

You can view the ERD here:  
[Library System ERD](https://dbdiagram.io/d/Library-System-68dcccbfd2b621e422b9246d)

