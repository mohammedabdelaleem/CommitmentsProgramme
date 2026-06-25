using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommitmentsProgramme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeUnneccessaryDisplayOrderFromBranc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Branches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
