using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BooksContainCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategories_Books_BookTitle",
                table: "BookCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_BookCategories_BookTitle",
                table: "BookCategories");

            migrationBuilder.DropColumn(
                name: "BookTitle",
                table: "BookCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "BookCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_BookId",
                table: "BookCategories",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategories_Books_BookId",
                table: "BookCategories",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategories_Books_BookId",
                table: "BookCategories");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Title",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_BookCategories_BookId",
                table: "BookCategories");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "BookCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "BookTitle",
                table: "BookCategories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_BookTitle",
                table: "BookCategories",
                column: "BookTitle");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategories_Books_BookTitle",
                table: "BookCategories",
                column: "BookTitle",
                principalTable: "Books",
                principalColumn: "Title");
        }
    }
}
