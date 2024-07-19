using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class library : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCategoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryID",
                        column: x => x.ParentCategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCategoryID = table.Column<int>(type: "int", nullable: true),
                    SubCategoryID = table.Column<int>(type: "int", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Edition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_Books_Categories_MainCategoryID",
                        column: x => x.MainCategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Categories_SubCategoryID",
                        column: x => x.SubCategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.BookID, x.AuthorID });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorID", "Biography", "DateOfBirth", "Email", "Name", "Nationality" },
                values: new object[,]
                {
                    { 1, "Famous science fiction author.", new DateTime(1920, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "isaac.asimov@example.com", "Isaac Asimov", "American" },
                    { 2, "Famous mystery author.", new DateTime(1890, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "agatha.christie@example.com", "Agatha Christie", "British" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name", "ParentCategoryID" },
                values: new object[,]
                {
                    { 1, "Fiction", null },
                    { 2, "Non-Fiction", null }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Description", "Edition", "Format", "Language", "Location", "MainCategoryID", "Pages", "Price", "PublishedDate", "Publisher", "SubCategoryID", "Title" },
                values: new object[] { 2, "A mystery novel.", "1st", "Paperback", "English", "Shelf B2", 1, 256, 12.99m, new DateTime(1934, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Collins Crime Club", null, "Murder on the Orient Express" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name", "ParentCategoryID" },
                values: new object[,]
                {
                    { 3, "Science Fiction", 1 },
                    { 4, "Biographies", 2 }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "AuthorID", "BookID" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Description", "Edition", "Format", "Language", "Location", "MainCategoryID", "Pages", "Price", "PublishedDate", "Publisher", "SubCategoryID", "Title" },
                values: new object[] { 1, "A science fiction novel.", "1st", "Hardcover", "English", "Shelf A1", 3, 255, 15.99m, new DateTime(1951, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gnome Press", null, "Foundation" });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "AuthorID", "BookID" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorID",
                table: "BookAuthors",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_MainCategoryID",
                table: "Books",
                column: "MainCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_SubCategoryID",
                table: "Books",
                column: "SubCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryID",
                table: "Categories",
                column: "ParentCategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
