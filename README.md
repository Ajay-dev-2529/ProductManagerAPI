# Product Management 

# Overview

This project is a .NET Core API that exposes RESTful endpoints for managing products and seller details. It provides standard CRUD functionality for handling products, including decrementing and adding stock, and links products to sellers based on the seller ID. The API is designed using the Entity Framework (EF) Core with a Code First approach and uses SQL Server LocalDB for data storage. The project also includes unit tests written using XUnit to ensure that the API behaves as expected under various scenarios. The project ensures unique product IDs and follows industry-standard coding practices, ensuring maintainability and scalability.

# Requirements

  # Prerequisites
     To run and test this API, you'll need:

    .NET 8.0 SDK 
    A code editor (e.g., Visual Studio Code or Visual Studio)
    Entity Framework Core packages installed:
    Microsoft.EntityFrameworkCore
    Microsoft.EntityFrameworkCore.SqlServer
    Microsoft.EntityFrameworkCore.Tools
    SQL Server LocalDB (installed by default with Visual Studio)
    XUnit testing framework installed for running unit tests.
    Optional: Postman or any other API testing tool.

# Steps to Set Up
  # 1.Clone the Repository
      git clone https://github.com/Ajay-dev-2529/ProductManagerAPI.git
      cd product-api
      
  # 2.EF core intialisation and DB and Table creation
      -dotnet ef database update
      
  # 3.Do dotnet restore then dotnet build then dotnet run
    
    
    




