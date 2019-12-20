using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hometest.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Unit_Price = table.Column<double>(nullable: false),
                    GroupDataId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryDatas_GroupDatas_GroupDataId",
                        column: x => x.GroupDataId,
                        principalTable: "GroupDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDatas_GroupDataId",
                table: "InventoryDatas",
                column: "GroupDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryDatas");

            migrationBuilder.DropTable(
                name: "GroupDatas");
        }
    }
}
