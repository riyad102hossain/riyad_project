using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G_Store.Migrations
{
    /// <inheritdoc />
    public partial class CreateSaleAndSupplieTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cus_id = table.Column<int>(type: "int", nullable: false),
                    pro_id = table.Column<int>(type: "int", nullable: false),
                    S_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    P_Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_cus_id",
                        column: x => x.cus_id,
                        principalTable: "Customers",
                        principalColumn: "cus_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Inventorys_pro_id",
                        column: x => x.pro_id,
                        principalTable: "Inventorys",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplies",
                columns: table => new
                {
                    pro_id = table.Column<int>(type: "int", nullable: false),
                    sup_id = table.Column<int>(type: "int", nullable: false),
                    P_Quantity = table.Column<int>(type: "int", nullable: false),
                    SaleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplies", x => new { x.pro_id, x.sup_id });
                    table.ForeignKey(
                        name: "FK_Supplies_Inventorys_pro_id",
                        column: x => x.pro_id,
                        principalTable: "Inventorys",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supplies_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "SaleId");
                    table.ForeignKey(
                        name: "FK_Supplies_Suppliers_sup_id",
                        column: x => x.sup_id,
                        principalTable: "Suppliers",
                        principalColumn: "sup_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_cus_id",
                table: "Sales",
                column: "cus_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_pro_id",
                table: "Sales",
                column: "pro_id");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_SaleId",
                table: "Supplies",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_sup_id",
                table: "Supplies",
                column: "sup_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Supplies");

            migrationBuilder.DropTable(
                name: "Sales");
        }
    }
}
