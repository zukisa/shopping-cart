shopping-cart
- this is an assessment for BET Software

Steps to setup prjoect
- fork project to local machine
- get project script from this location: ShoppingCart\Infrastructure\DbFile
- execute project script to create database with tables and data
- load up ShoppingCart.sln in VS
- build project in following order: Common, Core, Core.Tests, Infrastructure
- change connection string to point to your local database, find appsettings.json file in the API project folder
- update both appsettings.Development.json and appsettings.Production.json with the correct connectionstring
