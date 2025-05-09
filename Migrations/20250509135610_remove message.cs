using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrueMatch.Migrations
{
    /// <inheritdoc />
    public partial class removemessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Accounts_ReceiverEmail",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Accounts_SenderEmail",
                table: "FriendRequests");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_ReceiverEmail",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_SenderEmail_ReceiverEmail",
                table: "FriendRequests");

            migrationBuilder.AlterColumn<string>(
                name: "SenderEmail",
                table: "FriendRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverEmail",
                table: "FriendRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AccountEmail",
                table: "FriendRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountEmail1",
                table: "FriendRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_AccountEmail",
                table: "FriendRequests",
                column: "AccountEmail");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_AccountEmail1",
                table: "FriendRequests",
                column: "AccountEmail1");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Accounts_AccountEmail",
                table: "FriendRequests",
                column: "AccountEmail",
                principalTable: "Accounts",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Accounts_AccountEmail1",
                table: "FriendRequests",
                column: "AccountEmail1",
                principalTable: "Accounts",
                principalColumn: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Accounts_AccountEmail",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Accounts_AccountEmail1",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_AccountEmail",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_AccountEmail1",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "AccountEmail",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "AccountEmail1",
                table: "FriendRequests");

            migrationBuilder.AlterColumn<string>(
                name: "SenderEmail",
                table: "FriendRequests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverEmail",
                table: "FriendRequests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SenderEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Accounts_ReceiverEmail",
                        column: x => x.ReceiverEmail,
                        principalTable: "Accounts",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Accounts_SenderEmail",
                        column: x => x.SenderEmail,
                        principalTable: "Accounts",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_ReceiverEmail",
                table: "FriendRequests",
                column: "ReceiverEmail");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderEmail_ReceiverEmail",
                table: "FriendRequests",
                columns: new[] { "SenderEmail", "ReceiverEmail" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverEmail",
                table: "Messages",
                column: "ReceiverEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderEmail",
                table: "Messages",
                column: "SenderEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Timestamp",
                table: "Messages",
                column: "Timestamp");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Accounts_ReceiverEmail",
                table: "FriendRequests",
                column: "ReceiverEmail",
                principalTable: "Accounts",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Accounts_SenderEmail",
                table: "FriendRequests",
                column: "SenderEmail",
                principalTable: "Accounts",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
