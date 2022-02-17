using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Data.Migrations
{
    public partial class ReworkWorkItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Employees_AssignToEmployeeId",
                table: "WorkItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Employees_CreateByEmployeeId",
                table: "WorkItems");

            migrationBuilder.RenameColumn(
                name: "CreateByEmployeeId",
                table: "WorkItems",
                newName: "CreateByUserId");

            migrationBuilder.RenameColumn(
                name: "AssignToEmployeeId",
                table: "WorkItems",
                newName: "AssignToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItems_CreateByEmployeeId",
                table: "WorkItems",
                newName: "IX_WorkItems_CreateByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItems_AssignToEmployeeId",
                table: "WorkItems",
                newName: "IX_WorkItems_AssignToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_AspNetUsers_AssignToUserId",
                table: "WorkItems",
                column: "AssignToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_AspNetUsers_CreateByUserId",
                table: "WorkItems",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_AspNetUsers_AssignToUserId",
                table: "WorkItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_AspNetUsers_CreateByUserId",
                table: "WorkItems");

            migrationBuilder.RenameColumn(
                name: "CreateByUserId",
                table: "WorkItems",
                newName: "CreateByEmployeeId");

            migrationBuilder.RenameColumn(
                name: "AssignToUserId",
                table: "WorkItems",
                newName: "AssignToEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItems_CreateByUserId",
                table: "WorkItems",
                newName: "IX_WorkItems_CreateByEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItems_AssignToUserId",
                table: "WorkItems",
                newName: "IX_WorkItems_AssignToEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Employees_AssignToEmployeeId",
                table: "WorkItems",
                column: "AssignToEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Employees_CreateByEmployeeId",
                table: "WorkItems",
                column: "CreateByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
