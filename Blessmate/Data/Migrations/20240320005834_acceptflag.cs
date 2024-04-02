using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blessmate.Data.Migrations
{
    /// <inheritdoc />
    public partial class acceptflag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearsExperience",
                table: "Therapists");

            migrationBuilder.AddColumn<bool>(
                name: "HasAccepted",
                table: "Appointments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAccepted",
                table: "Appointments");

            migrationBuilder.AddColumn<byte>(
                name: "YearsExperience",
                table: "Therapists",
                type: "INTEGER",
                nullable: true);
        }
    }
}
