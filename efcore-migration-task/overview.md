# Entity Framework Core Migrations Script Generator
**Entity Framework Core Migrations Script Generator** is a very simple extensions to make it easy
to generate migration script for projects using Entity Framework Core with Code-First. This tool internally calls **dotnet ef migrations script**.

## How to generate migration scripts
With this task it's very easy to generate migration scripts:

* Add Entity Framework Core Migrations Script Generator task to your build pipeline.
* Select the project where the database project.
* Enter the names of the database contexts.
* If your database context is defined in a library, you also need to select an executable project that is using this library as start-up project.
* You could also change the directory where the migrations scripts should be stored. By default they are stored in a folder named **migrations**.
* If you are using **.NET Core 3**, you could enable **Install dependencies for .NET Core 3** to auto install the global tool **dotnet-ef**.
* If you are using **.NET Core 2**, you may be able to build your application but you get an error when creating migration scripts. If that's the case you're probably using .NET Core 3 SDK which doesn't have built-in support to do this. To solve this, just add [Use .NET Core](https://docs.microsoft.com/en-gb/azure/devops/pipelines/tasks/tool/dotnet-core-tool-installer?view=azure-devops) before this task and select version 2.2.207 for instance.

When the build is completed you should have migrations scripts stored in the package. They named {{NameOfTheDatabaseContext}}.sql. Under most circumstances it's safe to run these migrations on every release even if you haven't done any changes.

## How to apply migrations to your databases
When you have your migration scripts ready you just need to apply them in a release pipeline. If you have your databases in Azure you could to like this:

* Add the task **Azure SQL Database Deployment** to your release pipeline.
* Enter the details of your database.
* In the option **SQL Script** select the migration script from the package.

If you have several databases, add a new task for each database.