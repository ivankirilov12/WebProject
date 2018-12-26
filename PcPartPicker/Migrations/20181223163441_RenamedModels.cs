using Microsoft.EntityFrameworkCore.Migrations;

namespace PcPartPicker.Migrations
{
    public partial class RenamedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuild_Cases_CaseId",
                table: "SystemBuild");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuild_Cpus_CpuId",
                table: "SystemBuild");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuild_Gpus_GpuId",
                table: "SystemBuild");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuild_Motherboard_MotherboardId",
                table: "SystemBuild");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuild_Rams_RamId",
                table: "SystemBuild");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuild_Storages_StorageId",
                table: "SystemBuild");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemBuild",
                table: "SystemBuild");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Motherboard",
                table: "Motherboard");

            migrationBuilder.RenameTable(
                name: "SystemBuild",
                newName: "SystemBuilds");

            migrationBuilder.RenameTable(
                name: "Motherboard",
                newName: "Motherboards");

            migrationBuilder.RenameIndex(
                name: "IX_SystemBuild_StorageId",
                table: "SystemBuilds",
                newName: "IX_SystemBuilds_StorageId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemBuild_RamId",
                table: "SystemBuilds",
                newName: "IX_SystemBuilds_RamId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemBuild_MotherboardId",
                table: "SystemBuilds",
                newName: "IX_SystemBuilds_MotherboardId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemBuild_GpuId",
                table: "SystemBuilds",
                newName: "IX_SystemBuilds_GpuId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemBuild_CpuId",
                table: "SystemBuilds",
                newName: "IX_SystemBuilds_CpuId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemBuild_CaseId",
                table: "SystemBuilds",
                newName: "IX_SystemBuilds_CaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemBuilds",
                table: "SystemBuilds",
                column: "SystemBuildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motherboards",
                table: "Motherboards",
                column: "MotherboardId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuilds_Cases_CaseId",
                table: "SystemBuilds",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "CaseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuilds_Cpus_CpuId",
                table: "SystemBuilds",
                column: "CpuId",
                principalTable: "Cpus",
                principalColumn: "CpuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuilds_Gpus_GpuId",
                table: "SystemBuilds",
                column: "GpuId",
                principalTable: "Gpus",
                principalColumn: "GpuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuilds_Motherboards_MotherboardId",
                table: "SystemBuilds",
                column: "MotherboardId",
                principalTable: "Motherboards",
                principalColumn: "MotherboardId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuilds_Rams_RamId",
                table: "SystemBuilds",
                column: "RamId",
                principalTable: "Rams",
                principalColumn: "RamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuilds_Storages_StorageId",
                table: "SystemBuilds",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "StorageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuilds_Cases_CaseId",
                table: "SystemBuilds");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuilds_Cpus_CpuId",
                table: "SystemBuilds");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuilds_Gpus_GpuId",
                table: "SystemBuilds");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuilds_Motherboards_MotherboardId",
                table: "SystemBuilds");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuilds_Rams_RamId",
                table: "SystemBuilds");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemBuilds_Storages_StorageId",
                table: "SystemBuilds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemBuilds",
                table: "SystemBuilds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Motherboards",
                table: "Motherboards");

            migrationBuilder.RenameTable(
                name: "SystemBuilds",
                newName: "SystemBuild");

            migrationBuilder.RenameTable(
                name: "Motherboards",
                newName: "Motherboard");

            migrationBuilder.RenameIndex(
                name: "IX_SystemBuilds_StorageId",
                table: "SystemBuild",
                newName: "IX_SystemBuild_StorageId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemBuilds_RamId",
                table: "SystemBuild",
                newName: "IX_SystemBuild_RamId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemBuilds_MotherboardId",
                table: "SystemBuild",
                newName: "IX_SystemBuild_MotherboardId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemBuilds_GpuId",
                table: "SystemBuild",
                newName: "IX_SystemBuild_GpuId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemBuilds_CpuId",
                table: "SystemBuild",
                newName: "IX_SystemBuild_CpuId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemBuilds_CaseId",
                table: "SystemBuild",
                newName: "IX_SystemBuild_CaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemBuild",
                table: "SystemBuild",
                column: "SystemBuildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motherboard",
                table: "Motherboard",
                column: "MotherboardId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuild_Cases_CaseId",
                table: "SystemBuild",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "CaseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuild_Cpus_CpuId",
                table: "SystemBuild",
                column: "CpuId",
                principalTable: "Cpus",
                principalColumn: "CpuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuild_Gpus_GpuId",
                table: "SystemBuild",
                column: "GpuId",
                principalTable: "Gpus",
                principalColumn: "GpuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuild_Motherboard_MotherboardId",
                table: "SystemBuild",
                column: "MotherboardId",
                principalTable: "Motherboard",
                principalColumn: "MotherboardId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuild_Rams_RamId",
                table: "SystemBuild",
                column: "RamId",
                principalTable: "Rams",
                principalColumn: "RamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemBuild_Storages_StorageId",
                table: "SystemBuild",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "StorageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
