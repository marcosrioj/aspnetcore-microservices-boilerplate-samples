This project was bootstrapped with [AspnetBoilerplate](https://github.com/aspnetboilerplate/aspnetboilerplate).

## Overview
This is a default .net core 3.1 project.
It was designed to be a microservice model. Each microservice has access to authentication and roles validation. It is enough to identify the attributes in the API's.

There are microservices:
IdentityServer: Responsible for authentication, user management, tenants and sessions.
MicroserviceSample: A microservice model must be copied to create new ones. There is an API implemented for product management that exemplifies codes that are commonly used.

## Initial setting
Create the databases on the SqlServer (Or one that Efcore supports) SharedDb and MicroserviceSampleDb.

Run the migrations from the IdentityServer microservice and then MicroserviceSample. It can be done by the respective Migrations project, by efcore in visual studio or by dotnet-cli.

Place the IdentityServer.Web.Host and MicroserviceSample.Web.Host projects as the default Visual Studio project and start the project.

## DOC
AspnetBoilerplate documentation (https://aspnetboilerplate.com/Pages/Documents)