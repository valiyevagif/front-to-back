using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bigon.WebUI.Migrations
{
    public partial class BigonUserTokenForRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                schema: "Membership",
                table: "UserTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                schema: "Membership",
                table: "UserTokens",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Membership",
                table: "UserTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                schema: "Membership",
                table: "UserTokens",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                schema: "Membership",
                table: "UserTokens",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                schema: "Membership",
                table: "UserTokens",
                columns: new[] { "UserId", "LoginProvider", "Type", "Value" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                schema: "Membership",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Membership",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                schema: "Membership",
                table: "UserTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Membership",
                table: "UserTokens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                schema: "Membership",
                table: "UserTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                schema: "Membership",
                table: "UserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });
        }
    }
}
