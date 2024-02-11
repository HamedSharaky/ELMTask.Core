using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELM.Core.Persistence.Migrations
{
    public partial class AddBookIndexedView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var script = new System.Text.StringBuilder();
            script.AppendLine(@"CREATE OR ALTER VIEW [dbo].[vwBook]");
            script.AppendLine(@"WITH SCHEMABINDING");
            script.AppendLine(@"AS");
            script.AppendLine(@"SELECT ");
            script.AppendLine(@"    [BookId] AS [Id], ");
            script.AppendLine(@"    [LastModified] AS [LastModified],");
            script.AppendLine(@"    JSON_VALUE([BookInfo], '$.BookTitle') AS [BookTitle],");
            script.AppendLine(@"    JSON_VALUE([BookInfo], '$.BookDescription') AS [BookDescription],");
            script.AppendLine(@"    JSON_VALUE([BookInfo], '$.Author') AS [Author],");
            script.AppendLine(@"    JSON_VALUE([BookInfo], '$.PublishDate') AS [PublishDate]");
            script.AppendLine(@"FROM [dbo].[Book]");

            migrationBuilder.Sql(script.ToString());

            migrationBuilder.Sql("CREATE UNIQUE CLUSTERED INDEX IX_vwBook_Id ON [dbo].[vwBook](Id)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW [dbo].[vwBook]");
        }
    }
}
