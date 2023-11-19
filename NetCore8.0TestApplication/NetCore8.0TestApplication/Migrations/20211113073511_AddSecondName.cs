using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCore8TestApplication.Migrations
{
    public partial class AddSecondName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "FirstDataModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "FirstDataModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "FirstDataModels",
                keyColumn: "FirstDataModelID",
                keyValue: 1,
                columns: new[] { "LastName", "SecondName" },
                values: new object[] { "", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "FirstDataModels");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "FirstDataModels");
        }
    }
}
