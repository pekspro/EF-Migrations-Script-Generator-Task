using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreTestApplication.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FirstDataModels",
                columns: new[] { "FirstDataModelID", "LastName", "Name" },
                values: new object[] { 1, "Last name", "First name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FirstDataModels",
                keyColumn: "FirstDataModelID",
                keyValue: 1);
        }
    }
}
