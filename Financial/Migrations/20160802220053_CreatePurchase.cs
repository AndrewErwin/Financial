using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Financial.Migrations
{
    public partial class CreatePurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_CreditCard_CreditCardNetwork_NetworkId", table: "CreditCards");
            migrationBuilder.DropForeignKey(name: "FK_CreditCard_User_OwnerId", table: "CreditCards");
            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    CreditCardId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    InstalmentSplit = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    PurchasedOn = table.Column<DateTime>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchase_CreditCard_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Purchase_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.AddForeignKey(
                name: "FK_CreditCard_CreditCardNetwork_NetworkId",
                table: "CreditCards",
                column: "NetworkId",
                principalTable: "CreditCardNetworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_CreditCard_User_OwnerId",
                table: "CreditCards",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_CreditCard_CreditCardNetwork_NetworkId", table: "CreditCards");
            migrationBuilder.DropForeignKey(name: "FK_CreditCard_User_OwnerId", table: "CreditCards");
            migrationBuilder.DropTable("Purchases");
            migrationBuilder.AddForeignKey(
                name: "FK_CreditCard_CreditCardNetwork_NetworkId",
                table: "CreditCards",
                column: "NetworkId",
                principalTable: "CreditCardNetworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_CreditCard_User_OwnerId",
                table: "CreditCards",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
