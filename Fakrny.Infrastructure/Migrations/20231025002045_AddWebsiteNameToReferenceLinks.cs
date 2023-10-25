using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fakrny.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWebsiteNameToReferenceLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WebsiteName",
                table: "ReferenceLinks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebsiteName",
                table: "ReferenceLinks");
        }
    }
}
