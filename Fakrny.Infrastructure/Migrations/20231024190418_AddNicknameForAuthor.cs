using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fakrny.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNicknameForAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Authors");
        }
    }
}
