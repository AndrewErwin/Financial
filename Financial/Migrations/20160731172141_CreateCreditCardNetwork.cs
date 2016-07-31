using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Financial.Migrations
{
    public partial class CreateCreditCardNetwork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCardNetworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    DisplayName = table.Column<string>(nullable: false),
                    ImageName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCardNetwork", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("CreditCardNetworks");
        }
    }
}