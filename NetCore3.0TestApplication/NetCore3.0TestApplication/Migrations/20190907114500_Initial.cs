using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCore3_0TestApplication.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FirstDataModels",
                columns: table => new
                {
                    FirstDataModelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstDataModels", x => x.FirstDataModelID);
                });

            migrationBuilder.InsertData(
                table: "FirstDataModels",
                columns: new[] { "FirstDataModelID", "LastName", "Name" },
                values: new object[] { 1, "Last name", "First name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirstDataModels");
        }
    }
}
