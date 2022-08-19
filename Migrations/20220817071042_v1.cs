using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "goods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountPresent = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pictures_goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "goods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_pictures_GoodsId",
                table: "pictures",
                column: "GoodsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pictures");

            migrationBuilder.DropTable(
                name: "goods");
        }
    }
}
