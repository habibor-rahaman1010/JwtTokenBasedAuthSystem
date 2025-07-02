# JwtTokenBasedAuthSystem

## Project Setup Guideline

### This project demonstrates how to build a secure JWT-based authentication system using ASP.NET Core Web API. It allows users to register, log in, and obtain JWT tokens for accessing protected resources. The system uses role-based authorization to restrict access to certain API endpoints.

1. **Clone the Repository**  
   `https://github.com/habibor-rahaman1010/JwtTokenBasedAuthSystem`

2. **Create a Database in SQL Server Management Studio (SSMS)**  
   - Open SSMS.  
   - Create a new database and name it **JwtTokenBasedAuthSystem** .

4. **Run the EF-Core Migration for ASP.NET Identity**  
   - In Visual Studio’s **Package Manager Console** (or any terminal), run:  
   
     "dotnet ef database update -c "ApplicationDbContext" -p "Account.Management.Web""
   
   - This command adds the seven built-in ASP.NET Identity tables to the same database.

That’s it—run the project and navigate using the navigation bar on the first page.

---

### Roles

| Role | Username | Password | Can Access |
|------|----------|----------|------------|
| **Admin** | `user.admin@gmail.com` | `admin123` | **AddUser**, **AddRole**, **AssignAccess**, **AssignUserRole** |
| **Accountant** | `user.support@gmail.com` | `support123` | **ChartOfAccounts**, **VoucherList** |
