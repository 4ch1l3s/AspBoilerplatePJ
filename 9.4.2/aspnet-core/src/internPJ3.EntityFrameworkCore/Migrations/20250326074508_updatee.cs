using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace internPJ3.Migrations
{
    /// <inheritdoc />
    public partial class updatee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Cart_CartEId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Product_ProductFId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartEId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductFId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CartEId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ProductFId",
                table: "CartItems");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Cart_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Product_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Cart_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Product_ProductId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "CartEId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductFId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartEId",
                table: "CartItems",
                column: "CartEId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductFId",
                table: "CartItems",
                column: "ProductFId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Cart_CartEId",
                table: "CartItems",
                column: "CartEId",
                principalTable: "Cart",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Product_ProductFId",
                table: "CartItems",
                column: "ProductFId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
