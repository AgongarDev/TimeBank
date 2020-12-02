using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeBank.Core.Migrations
{
    public partial class FixedTokenDS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_User_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_User_TBankUserID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_User_ProviderID",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Token_Services_ServiceID",
                table: "Token");

            migrationBuilder.DropForeignKey(
                name: "FK_Token_Wallets_WalletID",
                table: "Token");

            migrationBuilder.DropForeignKey(
                name: "FK_Token_Wallets_WalletID1",
                table: "Token");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Addresses_AddressId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Validations_User_TBankUserID",
                table: "Validations");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_User_TBankUserID",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Token",
                table: "Token");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Token",
                newName: "Tokens");

            migrationBuilder.RenameIndex(
                name: "IX_User_AddressId",
                table: "Users",
                newName: "IX_Users_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Token_WalletID1",
                table: "Tokens",
                newName: "IX_Tokens_WalletID1");

            migrationBuilder.RenameIndex(
                name: "IX_Token_WalletID",
                table: "Tokens",
                newName: "IX_Tokens_WalletID");

            migrationBuilder.RenameIndex(
                name: "IX_Token_ServiceID",
                table: "Tokens",
                newName: "IX_Tokens_ServiceID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_TBankUserID",
                table: "Payments",
                column: "TBankUserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_ProviderID",
                table: "Services",
                column: "ProviderID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Services_ServiceID",
                table: "Tokens",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "ServiceID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Wallets_WalletID",
                table: "Tokens",
                column: "WalletID",
                principalTable: "Wallets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Wallets_WalletID1",
                table: "Tokens",
                column: "WalletID1",
                principalTable: "Wallets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Validations_Users_TBankUserID",
                table: "Validations",
                column: "TBankUserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_TBankUserID",
                table: "Wallets",
                column: "TBankUserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_TBankUserID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_ProviderID",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Services_ServiceID",
                table: "Tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Wallets_WalletID",
                table: "Tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Wallets_WalletID1",
                table: "Tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Validations_Users_TBankUserID",
                table: "Validations");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_TBankUserID",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Tokens",
                newName: "Token");

            migrationBuilder.RenameIndex(
                name: "IX_Users_AddressId",
                table: "User",
                newName: "IX_User_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Tokens_WalletID1",
                table: "Token",
                newName: "IX_Token_WalletID1");

            migrationBuilder.RenameIndex(
                name: "IX_Tokens_WalletID",
                table: "Token",
                newName: "IX_Token_WalletID");

            migrationBuilder.RenameIndex(
                name: "IX_Tokens_ServiceID",
                table: "Token",
                newName: "IX_Token_ServiceID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Token",
                table: "Token",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_User_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_User_TBankUserID",
                table: "Payments",
                column: "TBankUserID",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_User_ProviderID",
                table: "Services",
                column: "ProviderID",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Token_Services_ServiceID",
                table: "Token",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "ServiceID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Token_Wallets_WalletID",
                table: "Token",
                column: "WalletID",
                principalTable: "Wallets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Token_Wallets_WalletID1",
                table: "Token",
                column: "WalletID1",
                principalTable: "Wallets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Addresses_AddressId",
                table: "User",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Validations_User_TBankUserID",
                table: "Validations",
                column: "TBankUserID",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_User_TBankUserID",
                table: "Wallets",
                column: "TBankUserID",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
