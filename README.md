

## Create Project

- create ClassLibrary
- create ConsoleApp
- create WebAPI
- dotnet sln add MiniPOS.Database/MiniPOS.Database.csproj
- dotnet sln add MiniPOS.WebAPI/MiniPOS.WebAPI.csproj

- Mac တွင် SQL Server ကို Docker ဖြင့် Run ထားသည်ဆိုပါက၊ Connection String တွင် Server=localhost,1433 ဟု သုံးရပါမည်။

## Installation

Install Nuget Package (MiniPOS.Database )

```bash
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer
- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet add package Microsoft.EntityFrameworkCore.Tools
```

##  Scaffold Command: ( only single line )

dotnet ef dbcontext scaffold 'Server=localhost,1433;Database=MiniPOS;User Id=sa;Password=pps@Password123;TrustServerCertificate=True;' Microsoft.EntityFrameworkCore.SqlServer --output-dir AppDbContextModels --context AppDbContext --force
    

## Create Database, Table

- with Scaffold Command to auto Create model

## Connection 

- appsetting.json 
 "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=MiniPOS;Trusted_Connection=True;TrustServerCertificate=True;User Id=sa;Password=pps@Password123;"
  },



