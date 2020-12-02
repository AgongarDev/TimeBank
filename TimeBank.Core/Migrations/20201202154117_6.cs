using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeBank.Core.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Validations_ValidationId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Validations_Users_TBankUserID",
                table: "Validations");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Validations_ValidationId",
                table: "Payments",
                column: "ValidationId",
                principalTable: "Validations",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Validations_Users_TBankUserID",
                table: "Validations",
                column: "TBankUserID",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Validations_ValidationId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Validations_Users_TBankUserID",
                table: "Validations");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Validations_ValidationId",
                table: "Payments",
                column: "ValidationId",
                principalTable: "Validations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Validations_Users_TBankUserID",
                table: "Validations",
                column: "TBankUserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
