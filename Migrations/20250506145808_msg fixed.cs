using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrueMatch.Migrations
{
    /// <inheritdoc />
    public partial class msgfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_ReceiverEmail",
                table: "FriendRequests",
                column: "ReceiverEmail");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderEmail_ReceiverEmail",
                table: "FriendRequests",
                columns: new[] { "SenderEmail", "ReceiverEmail" },
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Accounts_ReceiverEmail",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Accounts_SenderEmail",
                table: "FriendRequests");

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
        }
    }
}
