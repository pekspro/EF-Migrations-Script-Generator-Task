using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreTestApplication.Migrations.SecondDatabase
{
    public partial class AddsCompanyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "SecondDataModels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "SecondDataModels");
        }
    }
}
