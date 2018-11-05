# Entity Framework Core Migrations Script Generator
**Entity Framework Core Migrations Script Generator** is a very simple extensions to make it easy
to generate migration script for projects using Entity Framework Core with Code-First. This tool internally calls **dotnet ef migrations script**.

## How to generate migration scripts
With this task it's very easy to generate migration scripts:

* Add Entity Framework Core Migrations Script Generator task to your build pipeline.
* Select a project that is using the database contexts. **Note:** If you have your database contexts in a library, select a project that is using that library.
* Enter the names of the database contexts.
* You could also change the directory where the migrations scripts should be stored. By default they are stored in a folder named **migrations**.

When the build is completed you should have migrations scripts stored in the package. They named {{NameOfTheDatabaseContext}}.sql. The migrations scripts are idempotent, meaning that you could run the several times and the end result should be the same even if you have run the script before. So it's safe to run the migration on every release even if you haven't done any changes.

## How to apply migrations to your databases
When you have your migration scripts ready you just need to apply them in a release pipeline. If you have your databases in Azure you could to like this:

* Add the task **Azure SQL Database Deployment** to your release pipeline.
* Enter the details of your database.
* In the option **SQL Script** select the migration script from the package.

If you have several databases, add a new task for each database.