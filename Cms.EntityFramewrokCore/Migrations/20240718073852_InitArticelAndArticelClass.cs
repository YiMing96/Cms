using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.EntityFramewrokCore.Migrations
{
    /// <inheritdoc />
    public partial class InitArticelAndArticelClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_ArticelClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticelClassName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ArticelClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_ArticelInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyWords = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TitleType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Intro = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TitleFontColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TitleFontType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArticelContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ArticelClassId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ArticelInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_ArticelInfos_T_ArticelClasses_ArticelClassId",
                        column: x => x.ArticelClassId,
                        principalTable: "T_ArticelClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_ArticelInfos_ArticelClassId",
                table: "T_ArticelInfos",
                column: "ArticelClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_ArticelInfos");

            migrationBuilder.DropTable(
                name: "T_ArticelClasses");
        }
    }
}
