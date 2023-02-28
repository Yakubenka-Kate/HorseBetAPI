using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HorseBet.Migrations
{
    /// <inheritdoc />
    public partial class FullVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c645053-3e42-4b6c-ab06-8238a07fe5d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad8d422f-4363-4cb7-b351-52dba4c7f40b");

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0018cb86-fd06-4f69-930c-17c34f1e9d30", "b2cfe4dd-613c-470e-a00c-912fe60ffd41", "User", "USER" },
                    { "b7f6afd3-c8d1-4711-99af-ad336794c8a4", "7fbd6c1f-2ae3-46ba-b8e4-fdbed6b5b2a6", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0018cb86-fd06-4f69-930c-17c34f1e9d30");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7f6afd3-c8d1-4711-99af-ad336794c8a4");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8c645053-3e42-4b6c-ab06-8238a07fe5d2", "6b33c983-e8c4-458c-bfec-73957c93b32d", "User", "USER" },
                    { "ad8d422f-4363-4cb7-b351-52dba4c7f40b", "23f661b6-3be8-4dc8-841e-f9494a003471", "Admin", "Admin" }
                });
        }
    }
}
