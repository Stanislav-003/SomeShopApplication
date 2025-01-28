using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SomeShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SomeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Products_ProductId1",
                table: "PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Purchases_PurchaseId1",
                table: "PurchaseItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseItems_ProductId1",
                table: "PurchaseItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseItems_PurchaseId1",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "PurchaseId1",
                table: "PurchaseItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "PurchaseItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseId1",
                table: "PurchaseItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_ProductId1",
                table: "PurchaseItems",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_PurchaseId1",
                table: "PurchaseItems",
                column: "PurchaseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Products_ProductId1",
                table: "PurchaseItems",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Purchases_PurchaseId1",
                table: "PurchaseItems",
                column: "PurchaseId1",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
