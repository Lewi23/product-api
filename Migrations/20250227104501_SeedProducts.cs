using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace product_api.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { "Product 1", 9.99M, 1 },
                    { "Product 2", 49.99M, 10 },
                    { "Product 3", 99.99M, 25 },
                    { "Product 4", 299.99M, 50 },
                    { "Product 5", 999.99M, 100 },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5
                }
            );
        }
    }
}