# EF Core Migrations Script Generator
**EF Core Migrations Script Generator** is a very simple extensions to make it easy
to generate migration script for projects using EF Core with Code-First. You select a project, a 
target directory and enter which database contexts to be the source of the
migration script. This tool internally calls **dotnet ef migrations script**.

The generate scripts could then be used to update a database. For instance with the
task **Azure SQL Database Deployment**.
