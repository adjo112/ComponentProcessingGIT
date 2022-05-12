using Microsoft.EntityFrameworkCore.Migrations;

namespace ComponentProcessing.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CredDetails",
                columns: table => new
                {
                    CreditcardNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Creditlimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredDetails", x => x.CreditcardNo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CredDetails");
        }
    }
}
