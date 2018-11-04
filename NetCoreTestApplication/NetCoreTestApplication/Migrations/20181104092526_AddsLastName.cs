using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreTestApplication.Migrations
{
    public partial class AddsLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "FirstDataModels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "FirstDataModels");
        }
    }
}
