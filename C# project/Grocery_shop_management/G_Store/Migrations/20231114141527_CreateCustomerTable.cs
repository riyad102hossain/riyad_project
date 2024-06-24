using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G_Store.Migrations
{
    /// <inheritdoc />
    public partial class CreateCustomerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    cus_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cus_add = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cus_contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    due = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.cus_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
