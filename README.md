shopping-cart
- this is an assessment for BET Software

Steps to setup prjoect
- fork project to local machine
- get project script from this location: ShoppingCart\Infrastructure\DbFile
- execute project script to create database with tables and data
- load up ShoppingCart.sln in VS
- build project in following order: Common, Core, Core.Tests, Infrastructure, API
- when building the api project, if you get an an error, please delete the API.xml file from the project, the system will generate a new one.
- change connection string to point to your local database, find appsettings.json file in the API project folder
- update both appsettings.Development.json and appsettings.Production.json with the correct connectionstring


Steps to setup front-end
- go to root of the shopping-cart-client project
- update packages to the lastest using the following command: npm install
- please note that your installed version of angular should be the same version as the project. 
- please use following command to update angular: npm install @angular/cli --g
