using Microsoft.EntityFrameworkCore.Migrations;

namespace lab2_web_api.Migrations
{
    public partial class EditedTaskk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tasks_TaskId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Comments",
                newName: "TaskkId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TaskId",
                table: "Comments",
                newName: "IX_Comments_TaskkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tasks_TaskkId",
                table: "Comments",
                column: "TaskkId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tasks_TaskkId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "TaskkId",
                table: "Comments",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TaskkId",
                table: "Comments",
                newName: "IX_Comments_TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tasks_TaskId",
                table: "Comments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
