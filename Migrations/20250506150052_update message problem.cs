using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrueMatch.Migrations
{
    /// <inheritdoc />
    public partial class updatemessageproblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Accounts_ReceiverEmail",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Accounts_SenderEmail",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Timestamp",
                table: "Messages",
                column: "Timestamp");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Accounts_ReceiverEmail",
                table: "Messages",
                column: "ReceiverEmail",
                principalTable: "Accounts",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Accounts_SenderEmail",
                table: "Messages",
                column: "SenderEmail",
                principalTable: "Accounts",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Accounts_ReceiverEmail",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Accounts_SenderEmail",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Timestamp",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Accounts_ReceiverEmail",
                table: "Messages",
                column: "ReceiverEmail",
                principalTable: "Accounts",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Accounts_SenderEmail",
                table: "Messages",
                column: "SenderEmail",
                principalTable: "Accounts",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
