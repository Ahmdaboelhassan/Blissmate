using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blessmate.Data.Migrations
{
    /// <inheritdoc />
    public partial class addTherapistCertificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertificateUrl",
                table: "Therapists",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateUrl",
                table: "Therapists");
        }
    }
}
