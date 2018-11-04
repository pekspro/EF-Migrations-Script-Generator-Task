# EF Core Migrations Script Generator
**EF Core Migrations Script Generator** is a very simple extensions to make it easy
to generate migration script for projects using EF Core with Code-First. You select a project, a 
target directory and enter which database contexts to be the source of the
migration script. This tool internally calls **dotnet ef migrations script**.

The generate scripts could then be used to update a database. For instance with the
task **Azure SQL Database Deployment**.

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

    tsc

This compiles the typescript file index.ts typescript file into index.js.

    $env:INPUT_PROJECTPATH="C:/Users/msn/Source/Repos/EF-Migrations-Script-Generator-Task/NetCoreTestApplication/NetCoreTestApplication/NetCoreTestApplication.csproj"
    $env:INPUT_TARGETFOLDER="c:/temp"
    $env:INPUT_DATABASECONTEXTS="FirstDatabaseContext`nSecondDatabaseContext"

These commands sets enviroment variables. These are used for setting input values for the script.

    node index.js

This executes the script.

## How to create a new release
To create a new release one single command is needed to create a vsix-file. This should be executed
in the **efcore-migration-task** directory:

    tfx extension create


# References
Mainfest description:
https://docs.microsoft.com/sv-se/azure/devops/extend/develop/manifest

Task.json reference:
https://raw.githubusercontent.com/Microsoft/vsts-task-lib/master/tasks.schema.json

