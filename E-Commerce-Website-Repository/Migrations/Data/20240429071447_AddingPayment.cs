using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Website_Repository.Migrations.Data
{
    public partial class AddingPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BasketId",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "orders");
        }
    }
}
