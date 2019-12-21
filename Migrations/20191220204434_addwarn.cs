using Microsoft.EntityFrameworkCore.Migrations;

namespace hometest.Migrations
{
    public partial class addwarn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Warning_Count",
                table: "InventoryDatas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Warning_Count",
                table: "InventoryDatas");
        }
    }
}
