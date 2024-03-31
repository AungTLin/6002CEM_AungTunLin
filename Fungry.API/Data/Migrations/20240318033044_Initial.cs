using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fungry.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodOptions",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Taste = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Extra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodOptions", x => new { x.FoodId, x.Taste, x.Extra });
                    table.ForeignKey(
                        name: "FK_FoodOptions_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Taste = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Extra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 2, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 3, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 4, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 5, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 6, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 7, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 8, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 9, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 10, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 11, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 12, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 13, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 14, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 },
                    { 15, "https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg", "Chicken Pizza", 15.199999999999999 }
                });

            migrationBuilder.InsertData(
                table: "FoodOptions",
                columns: new[] { "Extra", "FoodId", "Taste" },
                values: new object[,]
                {
                    { "Cheese", 1, "Default" },
                    { "Default", 2, "Spicy" },
                    { "Default", 3, "Spicy" },
                    { "Default", 4, "Spicy" },
                    { "Default", 5, "Spicy" },
                    { "Default", 6, "Spicy" },
                    { "Default", 7, "Spicy" },
                    { "Default", 8, "Spicy" },
                    { "Default", 9, "Spicy" },
                    { "Default", 10, "Spicy" },
                    { "Default", 11, "Spicy" },
                    { "Default", 12, "Spicy" },
                    { "Default", 13, "Spicy" },
                    { "Default", 14, "Spicy" },
                    { "Default", 15, "Spicy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodOptions");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
