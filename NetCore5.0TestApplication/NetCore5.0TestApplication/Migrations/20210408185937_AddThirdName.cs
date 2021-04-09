using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCore5TestApplication.Migrations
{
    public partial class AddThirdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThirdName",
                table: "FirstDataModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThirdName",
                table: "FirstDataModels");
        }
    }
}
