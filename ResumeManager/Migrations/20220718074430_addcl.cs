using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeManager.Migrations
{
    public partial class addcl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "Languages",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "Languages");
        }
    }
}
