using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrueMatch.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Accounts_Email",
                table: "UserInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_Email",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "UserInfos");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "UserInfos",
                newName: "ProfileImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "BackGroundImageUrl",
                table: "UserInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "BackGroundImageUrl",
                table: "UserInfos");

            migrationBuilder.RenameColumn(
                name: "ProfileImageUrl",
                table: "UserInfos",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "UserInfos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_Email",
                table: "UserInfos",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Accounts_Email",
                table: "UserInfos",
                column: "Email",
                principalTable: "Accounts",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
