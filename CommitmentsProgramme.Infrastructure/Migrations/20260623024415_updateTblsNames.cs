using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommitmentsProgramme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateTblsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commitments_DailyPlan_DailyPlanId",
                table: "Commitments");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyPlan_Officer_DutyOfficerId",
                table: "DailyPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyPlan_Officer_SeniorOfficerId",
                table: "DailyPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_Officer_Ranks_RankId",
                table: "Officer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Officer",
                table: "Officer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyPlan",
                table: "DailyPlan");

            migrationBuilder.RenameTable(
                name: "Officer",
                newName: "Officers");

            migrationBuilder.RenameTable(
                name: "DailyPlan",
                newName: "DailyPlans");

            migrationBuilder.RenameIndex(
                name: "IX_Officer_RankId",
                table: "Officers",
                newName: "IX_Officers_RankId");

            migrationBuilder.RenameIndex(
                name: "IX_DailyPlan_SeniorOfficerId",
                table: "DailyPlans",
                newName: "IX_DailyPlans_SeniorOfficerId");

            migrationBuilder.RenameIndex(
                name: "IX_DailyPlan_DutyOfficerId",
                table: "DailyPlans",
                newName: "IX_DailyPlans_DutyOfficerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Officers",
                table: "Officers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyPlans",
                table: "DailyPlans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Commitments_DailyPlans_DailyPlanId",
                table: "Commitments",
                column: "DailyPlanId",
                principalTable: "DailyPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPlans_Officers_DutyOfficerId",
                table: "DailyPlans",
                column: "DutyOfficerId",
                principalTable: "Officers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPlans_Officers_SeniorOfficerId",
                table: "DailyPlans",
                column: "SeniorOfficerId",
                principalTable: "Officers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Officers_Ranks_RankId",
                table: "Officers",
                column: "RankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commitments_DailyPlans_DailyPlanId",
                table: "Commitments");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyPlans_Officers_DutyOfficerId",
                table: "DailyPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyPlans_Officers_SeniorOfficerId",
                table: "DailyPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Officers_Ranks_RankId",
                table: "Officers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Officers",
                table: "Officers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyPlans",
                table: "DailyPlans");

            migrationBuilder.RenameTable(
                name: "Officers",
                newName: "Officer");

            migrationBuilder.RenameTable(
                name: "DailyPlans",
                newName: "DailyPlan");

            migrationBuilder.RenameIndex(
                name: "IX_Officers_RankId",
                table: "Officer",
                newName: "IX_Officer_RankId");

            migrationBuilder.RenameIndex(
                name: "IX_DailyPlans_SeniorOfficerId",
                table: "DailyPlan",
                newName: "IX_DailyPlan_SeniorOfficerId");

            migrationBuilder.RenameIndex(
                name: "IX_DailyPlans_DutyOfficerId",
                table: "DailyPlan",
                newName: "IX_DailyPlan_DutyOfficerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Officer",
                table: "Officer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyPlan",
                table: "DailyPlan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Commitments_DailyPlan_DailyPlanId",
                table: "Commitments",
                column: "DailyPlanId",
                principalTable: "DailyPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPlan_Officer_DutyOfficerId",
                table: "DailyPlan",
                column: "DutyOfficerId",
                principalTable: "Officer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPlan_Officer_SeniorOfficerId",
                table: "DailyPlan",
                column: "SeniorOfficerId",
                principalTable: "Officer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Officer_Ranks_RankId",
                table: "Officer",
                column: "RankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
