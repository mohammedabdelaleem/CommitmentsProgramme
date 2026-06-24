using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommitmentsProgramme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NullableCreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Commitments_CommitmentId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Commitments_CommitmentId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_CommitmentId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_CommitmentId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "CommitmentId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "CommitmentId",
                table: "Attendances");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Ranks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Priorities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Officers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "DailyPlans",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "CommitmentType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Commitments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CommitmentBranches",
                columns: table => new
                {
                    CommitmentId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitmentBranches", x => new { x.CommitmentId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_CommitmentBranches_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommitmentBranches_Commitments_CommitmentId",
                        column: x => x.CommitmentId,
                        principalTable: "Commitments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommitmentsAttendances",
                columns: table => new
                {
                    CommitmentId = table.Column<int>(type: "int", nullable: false),
                    AttendanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitmentsAttendances", x => new { x.CommitmentId, x.AttendanceId });
                    table.ForeignKey(
                        name: "FK_CommitmentsAttendances_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommitmentsAttendances_Commitments_CommitmentId",
                        column: x => x.CommitmentId,
                        principalTable: "Commitments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommitmentBranches_BranchId",
                table: "CommitmentBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CommitmentBranches_CommitmentId_BranchId",
                table: "CommitmentBranches",
                columns: new[] { "CommitmentId", "BranchId" });

            migrationBuilder.CreateIndex(
                name: "IX_CommitmentsAttendances_AttendanceId",
                table: "CommitmentsAttendances",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_CommitmentsAttendances_CommitmentId_AttendanceId",
                table: "CommitmentsAttendances",
                columns: new[] { "CommitmentId", "AttendanceId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommitmentBranches");

            migrationBuilder.DropTable(
                name: "CommitmentsAttendances");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Ranks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Priorities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Officers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "DailyPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "CommitmentType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Commitments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommitmentId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommitmentId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CommitmentId",
                table: "Branches",
                column: "CommitmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_CommitmentId",
                table: "Attendances",
                column: "CommitmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Commitments_CommitmentId",
                table: "Attendances",
                column: "CommitmentId",
                principalTable: "Commitments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Commitments_CommitmentId",
                table: "Branches",
                column: "CommitmentId",
                principalTable: "Commitments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
