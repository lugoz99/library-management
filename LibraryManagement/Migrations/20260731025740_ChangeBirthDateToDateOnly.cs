using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBirthDateToDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookFormat_Books_BookId",
                table: "BookFormat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookFormat",
                table: "BookFormat");

            migrationBuilder.RenameTable(
                name: "BookFormat",
                newName: "BookFormats");

            migrationBuilder.RenameIndex(
                name: "IX_BookFormat_BookId",
                table: "BookFormats",
                newName: "IX_BookFormats_BookId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PublicationDate",
                table: "Books",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfDeath",
                table: "Authors",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Authors",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookFormats",
                table: "BookFormats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookFormats_Books_BookId",
                table: "BookFormats",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookFormats_Books_BookId",
                table: "BookFormats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookFormats",
                table: "BookFormats");

            migrationBuilder.RenameTable(
                name: "BookFormats",
                newName: "BookFormat");

            migrationBuilder.RenameIndex(
                name: "IX_BookFormats_BookId",
                table: "BookFormat",
                newName: "IX_BookFormat_BookId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfDeath",
                table: "Authors",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Authors",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookFormat",
                table: "BookFormat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookFormat_Books_BookId",
                table: "BookFormat",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
