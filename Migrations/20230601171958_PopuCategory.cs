using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    /// <inheritdoc />
    public partial class PopuCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Category(Name, ImageUrl) VALUES('Drinks', 'Drinks.jpg')");
            migrationBuilder.Sql("INSERT INTO Category(Name, ImageUrl) VALUES('Food', 'Food.jpg')");
            migrationBuilder.Sql("INSERT INTO Category(Name, ImageUrl) VALUES('Desserts', 'Desserts.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Category");
        }
    }
}
