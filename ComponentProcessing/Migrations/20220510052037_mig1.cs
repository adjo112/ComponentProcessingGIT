using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComponentProcessing.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessRequest",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsPriorityRequest = table.Column<bool>(type: "bit", nullable: false),
                    OrderPlacedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessRequest", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "ProcessResponse",
                columns: table => new
                {
                    Id_res = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessingCharge = table.Column<double>(type: "float", nullable: false),
                    PackagingAndDeliveryCharge = table.Column<double>(type: "float", nullable: false),
                    DateOfDelivery = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalCharge = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessResponse", x => x.Id_res);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessRequest");

            migrationBuilder.DropTable(
                name: "ProcessResponse");
        }
    }
}
