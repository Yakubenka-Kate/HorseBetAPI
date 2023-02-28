using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HorseBet.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3557ea1f-032a-4a0e-891c-30775ca26e7c", "0b198958-ca7b-466c-ace0-3017fbe5ddef", "Admin", "Admin" },
                    { "da9a2983-6e6f-47a1-a4f3-b69fbabae8a5", "6fbbc69a-22a7-4632-aab5-7b94b8f878e8", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3557ea1f-032a-4a0e-891c-30775ca26e7c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da9a2983-6e6f-47a1-a4f3-b69fbabae8a5");
        }
    }
}
