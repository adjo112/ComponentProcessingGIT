using Microsoft.EntityFrameworkCore.Migrations;

namespace ComponentProcessing.Migrations
{
    public partial class Model_c_ProcReq_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessRequest",
                table: "ProcessRequest");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ProcessRequest",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "IdReq",
                table: "ProcessRequest",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessRequest",
                table: "ProcessRequest",
                column: "IdReq");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessRequest",
                table: "ProcessRequest");

            migrationBuilder.DropColumn(
                name: "IdReq",
                table: "ProcessRequest");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ProcessRequest",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessRequest",
                table: "ProcessRequest",
                column: "UserName");
        }
    }
}
