using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommitmentsProgramme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Trafficr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrafficPlaneId",
                table: "Ranks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrafficPlaneId",
                table: "Places",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrafficPlanes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateOnly = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficPlanes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ranks_TrafficPlaneId",
                table: "Ranks",
                column: "TrafficPlaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_TrafficPlaneId",
                table: "Places",
                column: "TrafficPlaneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_TrafficPlanes_TrafficPlaneId",
                table: "Places",
                column: "TrafficPlaneId",
                principalTable: "TrafficPlanes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ranks_TrafficPlanes_TrafficPlaneId",
                table: "Ranks",
                column: "TrafficPlaneId",
                principalTable: "TrafficPlanes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_TrafficPlanes_TrafficPlaneId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Ranks_TrafficPlanes_TrafficPlaneId",
                table: "Ranks");

            migrationBuilder.DropTable(
                name: "TrafficPlanes");

            migrationBuilder.DropIndex(
                name: "IX_Ranks_TrafficPlaneId",
                table: "Ranks");

            migrationBuilder.DropIndex(
                name: "IX_Places_TrafficPlaneId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "TrafficPlaneId",
                table: "Ranks");

            migrationBuilder.DropColumn(
                name: "TrafficPlaneId",
                table: "Places");
        }
    }
}
