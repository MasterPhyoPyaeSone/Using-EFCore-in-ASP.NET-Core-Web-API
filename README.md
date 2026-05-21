

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


    

## Create Database, Table

- with Scaffold Command to auto Create model




