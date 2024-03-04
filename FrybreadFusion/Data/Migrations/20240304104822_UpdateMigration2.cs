using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrybreadFusion.Data.Migrations
{
    public partial class UpdateMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Comments",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UserComment",
                table: "Comments",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DatePosted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CommentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Replies_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Replies",
                columns: new[] { "Id", "CommentId", "DatePosted", "Text", "UserName" },
                values: new object[] { 1, 1, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "I completely agree with your points!", "ReplyUser1" });

            migrationBuilder.InsertData(
                table: "Replies",
                columns: new[] { "Id", "CommentId", "DatePosted", "Text", "UserName" },
                values: new object[] { 2, 2, new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is a test post!", "ReplyUser2" });

            migrationBuilder.InsertData(
                table: "Replies",
                columns: new[] { "Id", "CommentId", "DatePosted", "Text", "UserName" },
                values: new object[] { 3, 3, new DateTime(2023, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "What an insightful post!", "ReplyUser3" });

            migrationBuilder.CreateIndex(
                name: "IX_Replies_CommentId",
                table: "Replies",
                column: "CommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "UserName",
                keyValue: null,
                column: "UserName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Comments",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "UserComment",
                keyValue: null,
                column: "UserComment",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserComment",
                table: "Comments",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
