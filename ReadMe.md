# Entity Framework Core Migrations Script Generator

**Entity Framework Core Migrations Script Generator** is a very simple extension
to make it easy to generate migration script for projects using Entity Framework
Core with Code-First. This tool internally calls **dotnet ef migrations
script**.

This tool can be installed from [Visual Studio
Marketplace](https://marketplace.visualstudio.com/items?itemName=pekspro.pekspro-efcore-migration-script-generator).

## How to generate migration scripts

With this task it's very easy to generate migration scripts:

* Add Entity Framework Core Migrations Script Generator task to your build
  pipeline.
* Select the project where the database project.
* Enter the names of the database contexts.
* If your database context is defined in a library, you also need to select an
  executable project that is using this library as start-up project.
* You could also change the directory where the migrations scripts should be
  stored. By default they are stored in a folder named **migrations**.
* If you are using **.NET Core 3** or later, you could enable **Install
  dependencies for .NET Core 3 or later** to auto install the global tool
  **dotnet-ef**.
* If you are using **.NET Core 2**, you may be able to build your application
  but you get an error when creating migration scripts. If that's the case
  you're probably using .NET Core 3 SDK which doesn't have built-in support to
  do this. To solve this, just add [Use .NET
  Core](https://docs.microsoft.com/en-gb/azure/devops/pipelines/tasks/tool/dotnet-core-tool-installer?view=azure-devops)
  before this task and select version 2.2.207 for instance.

When the build is completed you should have migrations scripts stored in the
package. They named {{NameOfTheDatabaseContext}}.sql. Under most circumstances
it's safe to run these migrations on every release even if you haven't done any
changes.

## How to apply migrations to your databases

When you have your migration scripts ready you just need to apply them in a
release pipeline. If you have your databases in Azure you could to like this:

* Add the task **Azure SQL Database Deployment** to your release pipeline.
* Enter the details of your database.
* In the option **SQL Script** select the migration script from the package.

If you have several databases, add a new task for each database.

## Supported versions

Both .NET Core 2 and later with Entity Framework 2 and later is supported. That
said, if you are using .NET Core 3.x or later this tool requires that the global
tool **dotnet-ef** is installed. You could do this easily be enable **Install
dependencies for .NET Core 3 or later**.

If you are using **.NET Core 2**, you may be able to build your application but
you get an error when creating migration scripts. If that's the case you
probably are using .NET Core 3 SDK which doesn't have build-in support to do
this. To solve this, just add [Use .NET
Core](https://docs.microsoft.com/en-gb/azure/devops/pipelines/tasks/tool/dotnet-core-tool-installer?view=azure-devops)
before this task and select version 2.2.207 for instance.

## Notes about the source

In the folder **efcore-migration-task** the complete source is for this project.

The folder **NetCoreTestApplication** contains a test project with two database
contexts that could be used for generating migration scripts.

## How to make changes

Changes are hopefully never needed :-) But if the logic need to be changes
index.ts should be modified. If the UI need to be changed task.json should be
updated.

## How to test changes (Windows)

There are some commands that are good to know to test the extension locally.
These should be executed in the folder
**efcore-migration-task/efcore-migration-script-generator**

This compiles the typescript file index.ts typescript file into index.js:

    tsc

These commands setup environment variables for a scenario where the database
context is defined in the executable project. These are used for setting input
values for the script.

    $env:INPUT_PROJECTPATH="C:/Users/msn/source/repos/pekspro/EF-Migrations-Script-Generator-Task/NetCore10.0TestApplication/NetCore10.0TestApplication/NetCore10.0TestApplication.csproj"
    $env:INPUT_TARGETFOLDER="c:/temp"
    $env:INPUT_DATABASECONTEXTS="FirstDatabaseContext`nSecondDatabaseContext"

These commands setup environment variables for a scenario where the database
context is defined in a library instead of the executable project:

    $env:INPUT_PROJECTPATH="C:/Users/msn/source/repos/pekspro/EF-Migrations-Script-Generator-Task/NetCore10.0TestApplication/NetCore10.0TestLibrary/NetCore10.0TestLibrary.csproj"
    $env:INPUT_STARTUPPROJECTPATH="C:/Users/msn/source/repos/pekspro/EF-Migrations-Script-Generator-Task/NetCore10.0TestApplication/NetCore10.0TestApplication/NetCore10.0TestApplication.csproj"
    $env:INPUT_TARGETFOLDER="c:/temp"
    $env:INPUT_DATABASECONTEXTS="LibraryDatabaseContext`nInternalLibraryDatabaseContext"
    $env:INPUT_NOTRANSACTION="true"

This executes the script.

    node index.js

## How to test changes (development container)

This repository includes a VS Code dev container configuration in the `.devcontainer` folder. It provides a container with .NET SDK, Node.js and `tfx-cli` installed so you can develop and test the task.

Start the dev container. Then enter these command to install node packges.

```bash
cd ./efcore-migration-task/efcore-migration-script-generator/
npm install
```

Run this to compile the typescript code:

```bash
tsc
```
Test when the database context is defined in the executable project:

```bash
export INPUT_PROJECTPATH="/workspace/NetCore10.0TestApplication/NetCore10.0TestApplication/NetCore10.0TestApplication.csproj"
export INPUT_TARGETFOLDER="/tmp/ef-migrations-output"
export INPUT_DATABASECONTEXTS=$'FirstDatabaseContext\nSecondDatabaseContext'
export INPUT_INSTALLDEPENDENCIES=true
export INPUT_EFTOOLVERSION=$'10.*'

dotnet restore $INPUT_PROJECTPATH

node index.js
```

Test when the database context is defined in a library and a startup project is required:

```bash
export INPUT_PROJECTPATH="/workspace/NetCore10.0TestApplication/NetCore10.0TestLibrary/NetCore10.0TestLibrary.csproj"
export INPUT_STARTUPPROJECTPATH="/workspace/NetCore10.0TestApplication/NetCore10.0TestApplication/NetCore10.0TestApplication.csproj"
export INPUT_TARGETFOLDER="/tmp/ef-migrations-output"
export INPUT_DATABASECONTEXTS=$'LibraryDatabaseContext\nInternalLibraryDatabaseContext'
export INPUT_NOTRANSACTION="true"
export INPUT_INSTALLDEPENDENCIES=true
export INPUT_EFTOOLVERSION=$'10.*'

dotnet restore $INPUT_PROJECTPATH

node index.js
```

The files are in the `tmp/ef-migrations-output` folder:

```bash
ls -l /tmp/ef-migrations-output || true
cat /tmp/ef-migrations-output/FirstDatabaseContext.sql || true
```

## How to create a new release

To create a new release one single command is needed to create a vsix-file. This
should be executed in the **efcore-migration-task** directory:

    tfx extension create

## Build and test status

Build:

[![Build
status](https://dev.azure.com/pekspro/EF-Migrations-Script-Generator-Task/_apis/build/status/Build%20extension)](https://dev.azure.com/pekspro/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=11)

Migration generator tests:

|.NET version | Windows | Linux |
|-------------|---------|-------|
| .NET 10     | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Windows%20.Net%2010?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=60&branchName=master) | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Linux%20.Net%2010?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=59&branchName=master) |
| .NET 9      | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Windows%20.Net%209?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=45&branchName=master) | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Linux%20.Net%209?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=44&branchName=master) |
| .NET 8      | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Windows%20.Net%208?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=47&branchName=master) | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Linux%20.Net%208?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=46&branchName=master) |
| .NET 6      | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Windows%20.Net%206?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=49&branchName=master) | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Linux%20.Net%206?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=48&branchName=master) |
| .NET 5      | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Windows%20.Net%205?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=51&branchName=master) | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Linux%20.Net%205?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=50&branchName=master) |
| .NET 3.1    | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Windows%20.Net%203.1?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=57&branchName=master) | |
| .NET 3.0    | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Windows%20.Net%203?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=53&branchName=master)  | |
| .NET 2.0    | [![Build Status](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_apis/build/status%2FTest%20Migration%20Windows%20.Net%202?branchName=master)](https://pekspro.visualstudio.com/EF-Migrations-Script-Generator-Task/_build/latest?definitionId=55&branchName=master)   | |

## References

* [Mainfest
  description](https://docs.microsoft.com/sv-se/azure/devops/extend/develop/manifest)
* [Task.json
  reference](https://raw.githubusercontent.com/Microsoft/vsts-task-lib/master/tasks.schema.json)
* [Azure Pipeline Tasks](https://github.com/Microsoft/azure-pipelines-tasks)

