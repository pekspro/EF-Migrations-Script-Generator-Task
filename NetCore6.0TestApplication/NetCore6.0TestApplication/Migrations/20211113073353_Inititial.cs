using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCore6TestApplication.Migrations
{
    public partial class Inititial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FirstDataModels",
                columns: table => new
                {
                    FirstDataModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstDataModels", x => x.FirstDataModelID);
                });

            migrationBuilder.InsertData(
                table: "FirstDataModels",
                columns: new[] { "FirstDataModelID", "Name" },
                values: new object[] { 1, "First name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirstDataModels");
        }
    }
}
