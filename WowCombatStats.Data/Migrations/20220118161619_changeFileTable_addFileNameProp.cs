using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WowCombatStats.Data.Migrations
{
    public partial class changeFileTable_addFileNameProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FileName",
                table: "Files",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Files");
        }
    }
}
