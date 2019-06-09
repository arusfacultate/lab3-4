using Microsoft.EntityFrameworkCore.Migrations;

namespace lab2_web_api.Migrations
{
    public partial class AddUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Uerrs",
                table: "Uerrs");

            migrationBuilder.RenameTable(
                name: "Uerrs",
                newName: "Users");

            migrationBuilder.AddColumn<int>(
                name: "AddedById",
                table: "Comments",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserRole",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AddedById",
                table: "Comments",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_AddedById",
                table: "Comments",
                column: "AddedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_AddedById",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AddedById",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Uerrs");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Uerrs",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Uerrs",
                table: "Uerrs",
                column: "Id");
        }
    }
}
