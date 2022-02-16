using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Data.Migrations
{
    public partial class AddWorkItemType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "WorkItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "WorkItems");
        }
    }
}
