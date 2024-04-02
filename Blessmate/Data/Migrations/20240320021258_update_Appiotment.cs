using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blessmate.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_Appiotment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAccepted",
                table: "Appointments");

            migrationBuilder.AddColumn<DateTime>(
                name: "Day",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Appointments");

            migrationBuilder.AddColumn<bool>(
                name: "HasAccepted",
                table: "Appointments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
