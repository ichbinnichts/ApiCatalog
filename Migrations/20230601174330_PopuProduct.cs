using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    /// <inheritdoc />
    public partial class PopuProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Product(\"Name\", \"Description\", \"Price\", \"ImageUrl\", \"Stock\", \"RecordDate\", \"CategoryId\") VALUES('Beer', 'Alcoholic beverage', 2.50, 'Beer.jpg', 50, now(), 1)");

            migrationBuilder.Sql("INSERT INTO Product(\"Name\", \"Description\", \"Price\", \"ImageUrl\", \"Stock\", \"RecordDate\", \"CategoryId\") VALUES('Vegan Sandwich', 'Sandwich with no meat', 1.20, 'VeganSandwich.jpg', 50, now(), 2)");

            migrationBuilder.Sql("INSERT INTO Product(\"Name\", \"Description\"," +
                " \"Price\", \"ImageUrl\", \"Stock\", \"RecordDate\", \"CategoryId\")" +
                " VALUES('Pudding', 'Milk-based dessert', 2, 'Pudding.jpg', 50, now(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Product");
        }
    }
}
