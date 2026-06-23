using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommitmentsProgramme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingAttendancePlaceTbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commitments_Branches_BranchId",
                table: "Commitments");

            migrationBuilder.DropColumn(
                name: "Attendance",
                table: "Commitments");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Commitments");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Commitments",
                newName: "PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Commitments_BranchId",
                table: "Commitments",
                newName: "IX_Commitments_PlaceId");

            migrationBuilder.AddColumn<int>(
                name: "CommitmentId",
                table: "Branches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CommitmentId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Commitments_CommitmentId",
                        column: x => x.CommitmentId,
                        principalTable: "Commitments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CommitmentId",
                table: "Branches",
                column: "CommitmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_CommitmentId",
                table: "Attendances",
                column: "CommitmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Commitments_CommitmentId",
                table: "Branches",
                column: "CommitmentId",
                principalTable: "Commitments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Commitments_Places_PlaceId",
                table: "Commitments",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Commitments_CommitmentId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Commitments_Places_PlaceId",
                table: "Commitments");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Branches_CommitmentId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "CommitmentId",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "PlaceId",
                table: "Commitments",
                newName: "BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Commitments_PlaceId",
                table: "Commitments",
                newName: "IX_Commitments_BranchId");

            migrationBuilder.AddColumn<string>(
                name: "Attendance",
                table: "Commitments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Commitments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Commitments_Branches_BranchId",
                table: "Commitments",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
