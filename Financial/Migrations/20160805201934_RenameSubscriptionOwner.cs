using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Financial.Migrations
{
    public partial class RenameSubscriptionOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.DropForeignKey(name: "FK_Subscription_User_UserId", table: "Subscriptions");
            migrationBuilder.DropColumn(name: "UserId", table: "Subscriptions");
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Subscriptions",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_User_OwnerId",
                table: "Subscriptions",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.DropForeignKey(name: "FK_Subscription_User_OwnerId", table: "Subscriptions");
            migrationBuilder.DropColumn(name: "OwnerId", table: "Subscriptions");
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Subscriptions",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_User_UserId",
                table: "Subscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
