using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommitmentsProgramme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCommitmentCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitmentBranches_Commitments_CommitmentId",
                table: "CommitmentBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_CommitmentsAttendances_Commitments_CommitmentId",
                table: "CommitmentsAttendances");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Time",
                table: "Commitments",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommitmentBranches_Commitments_CommitmentId",
                table: "CommitmentBranches",
                column: "CommitmentId",
                principalTable: "Commitments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommitmentsAttendances_Commitments_CommitmentId",
                table: "CommitmentsAttendances",
                column: "CommitmentId",
                principalTable: "Commitments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitmentBranches_Commitments_CommitmentId",
                table: "CommitmentBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_CommitmentsAttendances_Commitments_CommitmentId",
                table: "CommitmentsAttendances");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Time",
                table: "Commitments",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AddForeignKey(
                name: "FK_CommitmentBranches_Commitments_CommitmentId",
                table: "CommitmentBranches",
                column: "CommitmentId",
                principalTable: "Commitments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommitmentsAttendances_Commitments_CommitmentId",
                table: "CommitmentsAttendances",
                column: "CommitmentId",
                principalTable: "Commitments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
