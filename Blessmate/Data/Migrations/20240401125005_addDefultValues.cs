using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blessmate.Data.Migrations
{
    /// <inheritdoc />
    public partial class addDefultValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Therapists",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Therapists",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearsExperience",
                table: "Therapists",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "YearsExperience",
                table: "Therapists");
        }
    }
}
