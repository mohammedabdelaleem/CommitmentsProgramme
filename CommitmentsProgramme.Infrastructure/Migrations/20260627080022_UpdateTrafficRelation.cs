using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommitmentsProgramme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrafficRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Officers_TrafficPlanes_TrafficPlaneId",
                table: "Officers");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_TrafficPlanes_TrafficPlaneId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Ranks_TrafficPlanes_TrafficPlaneId",
                table: "Ranks");

            migrationBuilder.DropIndex(
                name: "IX_Ranks_TrafficPlaneId",
                table: "Ranks");

            migrationBuilder.DropIndex(
                name: "IX_Places_TrafficPlaneId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Officers_TrafficPlaneId",
                table: "Officers");

            migrationBuilder.DropColumn(
                name: "TrafficPlaneId",
                table: "Ranks");

            migrationBuilder.DropColumn(
                name: "TrafficPlaneId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "TrafficPlaneId",
                table: "Officers");

            migrationBuilder.CreateTable(
                name: "TrafficOfficers",
                columns: table => new
                {
                    TrafficPlaneId = table.Column<int>(type: "int", nullable: false),
                    OfficerId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficOfficers", x => new { x.TrafficPlaneId, x.OfficerId });
                    table.ForeignKey(
                        name: "FK_TrafficOfficers_Officers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "Officers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrafficOfficers_TrafficPlanes_TrafficPlaneId",
                        column: x => x.TrafficPlaneId,
                        principalTable: "TrafficPlanes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrafficPlaces",
                columns: table => new
                {
                    PlaceId = table.Column<int>(type: "int", nullable: false),
                    TrafficId = table.Column<int>(type: "int", nullable: false),
                    trafficPlaneId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficPlaces", x => new { x.TrafficId, x.PlaceId });
                    table.ForeignKey(
                        name: "FK_TrafficPlaces_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrafficPlaces_TrafficPlanes_trafficPlaneId",
                        column: x => x.trafficPlaneId,
                        principalTable: "TrafficPlanes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrafficOfficers_OfficerId",
                table: "TrafficOfficers",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficPlaces_PlaceId",
                table: "TrafficPlaces",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficPlaces_trafficPlaneId",
                table: "TrafficPlaces",
                column: "trafficPlaneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrafficOfficers");

            migrationBuilder.DropTable(
                name: "TrafficPlaces");

            migrationBuilder.AddColumn<int>(
                name: "TrafficPlaneId",
                table: "Ranks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrafficPlaneId",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrafficPlaneId",
                table: "Officers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ranks_TrafficPlaneId",
                table: "Ranks",
                column: "TrafficPlaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_TrafficPlaneId",
                table: "Places",
                column: "TrafficPlaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Officers_TrafficPlaneId",
                table: "Officers",
                column: "TrafficPlaneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Officers_TrafficPlanes_TrafficPlaneId",
                table: "Officers",
                column: "TrafficPlaneId",
                principalTable: "TrafficPlanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_TrafficPlanes_TrafficPlaneId",
                table: "Places",
                column: "TrafficPlaneId",
                principalTable: "TrafficPlanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ranks_TrafficPlanes_TrafficPlaneId",
                table: "Ranks",
                column: "TrafficPlaneId",
                principalTable: "TrafficPlanes",
                principalColumn: "Id");
        }
    }
}
