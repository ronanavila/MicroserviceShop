using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.ProductApi.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert Into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)" +
                " Values('Borracha branca', 7.55, 'Borracha Branca', 10,'BorrachaBranca.jpg', 1)");

            migrationBuilder.Sql("Insert Into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)" +
                " Values('Borracha Verde', 6.70, 'Borracha Verde', 11, 'BorrachaVerde.jpg', 1)");

            migrationBuilder.Sql("Insert Into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)" +
                " Values('Lapis Azul', 1.55, 'Lapis Azul', 12, 'LapisAzul.jpg', 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Products");
        }
    }
}
