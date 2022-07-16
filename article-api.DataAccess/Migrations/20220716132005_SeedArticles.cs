using Microsoft.EntityFrameworkCore.Migrations;

namespace article_api.DataAccess.Migrations
{
    public partial class SeedArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "articles",
                columns: new[] { "Id", "Title", "Text" },
                values: new object[,]
                    {
                        { "c8dfe6b2-54a3-4f26-99d9-9e9f02dc5a5c", "Some title", "Some text" },
                        { "b0bd61a6-44e2-4cff-b82b-3fade27ea66d", "Title test", "Text test" }
                    }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM articles");
        }
    }
}
