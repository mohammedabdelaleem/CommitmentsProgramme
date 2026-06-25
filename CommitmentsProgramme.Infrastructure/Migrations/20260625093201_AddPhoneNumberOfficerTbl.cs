using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommitmentsProgramme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberOfficerTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Officers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Officers");
        }
    }
}
