using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELM.Core.Persistence.Migrations
{
    public partial class AddBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.EnsureSchema(
            //    name: "dbo");

            //migrationBuilder.CreateTable(
            //    name: "Book",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        BookId = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        BookInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        LastModified = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Book", x => x.BookId);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Book_LastModified",
            //    schema: "dbo",
            //    table: "Book",
            //    column: "LastModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Book",
            //    schema: "dbo");
        }
    }
}
