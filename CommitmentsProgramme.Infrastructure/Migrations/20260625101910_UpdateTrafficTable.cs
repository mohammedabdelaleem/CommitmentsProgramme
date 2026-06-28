using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommitmentsProgramme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrafficTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_TrafficPlanes_TrafficPlaneId",
                table: "Places");

            migrationBuilder.AlterColumn<int>(
                name: "TrafficPlaneId",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrafficPlaneId",
                table: "Officers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Officers_TrafficPlanes_TrafficPlaneId",
                table: "Officers");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_TrafficPlanes_TrafficPlaneId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Officers_TrafficPlaneId",
                table: "Officers");

            migrationBuilder.DropColumn(
                name: "TrafficPlaneId",
                table: "Officers");

            migrationBuilder.AlterColumn<int>(
                name: "TrafficPlaneId",
                table: "Places",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_TrafficPlanes_TrafficPlaneId",
                table: "Places",
                column: "TrafficPlaneId",
                principalTable: "TrafficPlanes",
                principalColumn: "Id");
        }
    }
}
