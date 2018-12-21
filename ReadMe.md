# Entity Framework Core Migrations Script Generator
**Entity Framework Core Migrations Script Generator** is a very simple extension to make it easy
to generate migration script for projects using Entity Framework Core with Code-First. This tool internally calls **dotnet ef migrations script**.

This tool can be installed from https://marketplace.visualstudio.com/items?itemName=pekspro.pekspro-efcore-migration-script-generator.

## How to generate migration scripts
With this task it's very easy to generate migration scripts:

* Add Entity Framework Core Migrations Script Generator task to your build pipeline.
* Select the project where the database project.
* Enter the names of the database contexts.
* If your database context is defined in a library, you also need to select an executable project that is using this library as start-up project.
* You could also change the directory where the migrations scripts should be stored. By default they are stored in a folder named **migrations**.

When the build is completed you should have migrations scripts stored in the package. They named {{NameOfTheDatabaseContext}}.sql. The migrations scripts are idempotent, meaning that you could run the several times and the end result should be the same even if you have run the script before. So it's safe to run the migration on every release even if you haven't done any changes.

## How to apply migrations to your databases
When you have your migration scripts ready you just need to apply them in a release pipeline. If you have your databases in Azure you could to like this:

* Add the task **Azure SQL Database Deployment** to your release pipeline.
* Enter the details of your database.
* In the option **SQL Script** select the migration script from the package.

If you have several databases, add a new task for each database.



# Notes about the source
In the folder **efcore-migration-task** the complete source is for this project.

The folder **NetCoreTestApplication** contains a test project with two database contexts
that could be used for generating migration scripts.

# How to make changes
Changes are hopefully never needed :-) But if the logic need to be changes index.ts should be modified.
If the UI need to be changed task.json should be updated.

## How to test changes
There are some commands that are good to know to test the extension locally. These should be executed
in the folder **efcore-migration-task/efcore-migration-script-generator**

This compiles the typescript file index.ts typescript file into index.js:

    tsc

These commands setup environment variables for a scenario where the database context is defined in the executable project. These are used for setting input values for the script.

    $env:INPUT_PROJECTPATH="C:/Users/msn/Source/Repos/EF-Migrations-Script-Generator-Task/NetCoreTestApplication/NetCoreTestApplication/NetCoreTestApplication.csproj"
    $env:INPUT_TARGETFOLDER="c:/temp"
    $env:INPUT_DATABASECONTEXTS="FirstDatabaseContext`nSecondDatabaseContext"

These commands setup environment variables for a scenario where the database context is defined in a library instead of the executable project:

    $env:INPUT_PROJECTPATH="C:/Users/msn/Source/Repos/EF-Migrations-Script-Generator-Task/NetCoreTestApplication/NetCoreTestLibrary/NetCoreTestLibrary.csproj"
    $env:INPUT_STARTUPPROJECTPATH="C:/Users/msn/Source/Repos/EF-Migrations-Script-Generator-Task/NetCoreTestApplication/NetCoreTestApplication/NetCoreTestApplication.csproj"
    $env:INPUT_TARGETFOLDER="c:/temp"
    $env:INPUT_DATABASECONTEXTS="LibraryDatabaseContext`nInternalLibraryDatabaseContext"

This executes the script.

    node index.js



## How to create a new release
To create a new release one single command is needed to create a vsix-file. This should be executed
in the **efcore-migration-task** directory:

    tfx extension create


## Test status

Test in Windows:

[![Build status](https://dev.azure.com/pekspro/EF-Migrations-Script-Generator-Task/_apis/build/status/Test%20Migration%20Task%20-%20Windows)](https://dev.azure.com/pekspro/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=10)

Test in Linux:

[![Build status](https://dev.azure.com/pekspro/EF-Migrations-Script-Generator-Task/_apis/build/status/Test%20Migration%20Task%20-%20Linux)](https://dev.azure.com/pekspro/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=7)


# References
Mainfest description:
https://docs.microsoft.com/sv-se/azure/devops/extend/develop/manifest

Task.json reference:
https://raw.githubusercontent.com/Microsoft/vsts-task-lib/master/tasks.schema.json

Azure Pipeline Tasks:
https://github.com/Microsoft/azure-pipelines-tasks
