using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WowCombatStats.Data.Migrations
{
    public partial class ChangeFileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Raids_RaidId",
                table: "Files");

            migrationBuilder.AlterColumn<int>(
                name: "RaidId",
                table: "Files",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Raids_RaidId",
                table: "Files",
                column: "RaidId",
                principalTable: "Raids",
                principalColumn: "RaidId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Raids_RaidId",
                table: "Files");

            migrationBuilder.AlterColumn<int>(
                name: "RaidId",
                table: "Files",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Raids_RaidId",
                table: "Files",
                column: "RaidId",
                principalTable: "Raids",
                principalColumn: "RaidId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
