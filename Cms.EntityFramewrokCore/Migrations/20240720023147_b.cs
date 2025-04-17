using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.EntityFramewrokCore.Migrations
{
    /// <inheritdoc />
    public partial class b : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Changes",
                table: "T_ArticelInfos",
                type: "bigint",
                maxLength: 100,
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Changes",
                table: "T_ArticelInfos");
        }
    }
}
