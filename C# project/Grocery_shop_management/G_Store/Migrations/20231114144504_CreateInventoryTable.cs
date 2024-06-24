using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G_Store.Migrations
{
    /// <inheritdoc />
    public partial class CreateInventoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventorys",
                columns: table => new
                {
                    pro_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pro_cat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    selling_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventorys", x => x.pro_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventorys");
        }
    }
}
