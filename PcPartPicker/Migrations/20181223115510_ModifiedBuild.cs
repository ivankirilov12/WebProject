using Microsoft.EntityFrameworkCore.Migrations;

namespace PcPartPicker.Migrations
{
    public partial class ModifiedBuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuild_SystemBuild_SystemBuildId1",
                table: "SystemBuild");

            migrationBuilder.DropIndex(
                name: "IX_SystemBuild_SystemBuildId1",
                table: "SystemBuild");

            migrationBuilder.DropColumn(
                name: "SystemBuildId1",
                table: "SystemBuild");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SystemBuildId1",
                table: "SystemBuild",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemBuild_SystemBuildId1",
                table: "SystemBuild",
                column: "SystemBuildId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuild_SystemBuild_SystemBuildId1",
                table: "SystemBuild",
                column: "SystemBuildId1",
                principalTable: "SystemBuild",
                principalColumn: "SystemBuildId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
