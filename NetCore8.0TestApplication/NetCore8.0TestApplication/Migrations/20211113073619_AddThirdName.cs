using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCore8TestApplication.Migrations
{
    public partial class AddThirdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThirdName",
                table: "FirstDataModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "FirstDataModels",
                keyColumn: "FirstDataModelID",
                keyValue: 1,
                column: "ThirdName",
                value: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThirdName",
                table: "FirstDataModels");
        }
    }
}
