using Microsoft.EntityFrameworkCore.Migrations;

namespace WordMem.DataAccess.Migrations
{
    public partial class AddressEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Addresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "Addresses",
                nullable: false,
                defaultValue: 0);
        }
    }
}
