using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Experimentum.Api.Migrations
{
    public partial class phones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneType = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phone_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "dbo",
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phone_PersonId",
                table: "Phone",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phone");
        }
    }
}
