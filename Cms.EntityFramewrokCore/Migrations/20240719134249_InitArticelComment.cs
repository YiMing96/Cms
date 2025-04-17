using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.EntityFramewrokCore.Migrations
{
    /// <inheritdoc />
    public partial class InitArticelComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_ArticelComments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Msg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticelInfoId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ArticelComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_ArticelComments_T_ArticelInfos_ArticelInfoId",
                        column: x => x.ArticelInfoId,
                        principalTable: "T_ArticelInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_SensitiveWords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordPattern = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsForbid = table.Column<bool>(type: "bit", nullable: false),
                    IsMod = table.Column<bool>(type: "bit", nullable: false),
                    ReplaceWord = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SensitiveWords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_ArticelComments_ArticelInfoId",
                table: "T_ArticelComments",
                column: "ArticelInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_ArticelComments");

            migrationBuilder.DropTable(
                name: "T_SensitiveWords");
        }
    }
}
