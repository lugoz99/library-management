using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddWishListsAndItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Users_UserId1",
                table: "WishLists");

            migrationBuilder.DropTable(
                name: "WishListItem");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_UserId1",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "WishLists");

            migrationBuilder.CreateTable(
                name: "WishListItems",
                columns: table => new
                {
                    WishListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListItems", x => new { x.WishListId, x.BookId });
                    table.ForeignKey(
                        name: "FK_WishListItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListItems_WishLists_WishListId",
                        column: x => x.WishListId,
                        principalTable: "WishLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_BookId",
                table: "WishListItems",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishListItems");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "WishLists",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WishListItem",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WishListsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WishListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListItem", x => new { x.BooksId, x.WishListsId });
                    table.ForeignKey(
                        name: "FK_WishListItem_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WishListItem_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListItem_WishLists_WishListId",
                        column: x => x.WishListId,
                        principalTable: "WishLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WishListItem_WishLists_WishListsId",
                        column: x => x.WishListsId,
                        principalTable: "WishLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_UserId1",
                table: "WishLists",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItem_BookId",
                table: "WishListItem",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItem_WishListId_BookId",
                table: "WishListItem",
                columns: new[] { "WishListId", "BookId" },
                unique: true,
                filter: "[WishListId] IS NOT NULL AND [BookId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItem_WishListsId",
                table: "WishListItem",
                column: "WishListsId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Users_UserId1",
                table: "WishLists",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
