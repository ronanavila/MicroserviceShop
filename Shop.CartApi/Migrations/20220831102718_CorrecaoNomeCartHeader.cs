using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.CartApi.Migrations
{
    public partial class CorrecaoNomeCartHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CartHeaders_CartHeaderId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartHeaderId",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "CuponCode",
                table: "CartHeaders",
                newName: "CouponCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CouponCode",
                table: "CartHeaders",
                newName: "CuponCode");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartHeaderId",
                table: "CartItems",
                column: "CartHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CartHeaders_CartHeaderId",
                table: "CartItems",
                column: "CartHeaderId",
                principalTable: "CartHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
