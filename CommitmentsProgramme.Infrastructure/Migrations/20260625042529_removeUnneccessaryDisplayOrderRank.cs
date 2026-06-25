using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommitmentsProgramme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeUnneccessaryDisplayOrderRank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Ranks");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Priorities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Ranks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Priorities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
