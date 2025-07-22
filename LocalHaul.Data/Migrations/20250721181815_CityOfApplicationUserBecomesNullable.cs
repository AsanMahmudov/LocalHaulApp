using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class CityOfApplicationUserBecomesNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "The city where the user is located.",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "The city where the user is located.");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3782179a-c730-4e43-b163-094fef296752", "AQAAAAIAAYagAAAAEOxqaDb9nPFfV6KRFo2lW/cpRyi0k/pYEjncAPCqFkmRIsxAYVrI9QjFqKltOD4VzA==", "797f49a4-8bcb-4d69-b697-a7c98bb6208d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-3b4f-4f80-9b6e-4493d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ff65d19-9649-4482-826c-7d3b572a7c37", "AQAAAAIAAYagAAAAEKnasXjYPjw8/ajezwAaAVArpzXmGdp02/9XSr9bu0tT85FlH8dZl/6voBN7NtvbEA==", "bc85e24a-7685-4510-b6bd-cfa161cc72b9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "The city where the user is located.",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "The city where the user is located.");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a21aeed4-a9da-4572-8f4f-e1df704e0c0f", "AQAAAAIAAYagAAAAEKaR4Uze+zCH9uTy+CUypI7YF21wlOP+B3rv5t59xhS28syA1ukxAxrWhHj1Ru7+pQ==", "e13413ee-06d1-4121-a107-08c9fe5c8576" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-3b4f-4f80-9b6e-4493d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71b16a9b-5eab-41f1-8d28-19d2110f8e60", "AQAAAAIAAYagAAAAEDPRebFd/HL8a0q50fjpZnf0qevOw4MF2mFAK/nXuc5fOXL7Sx7CIH+pVNuJYz9TbA==", "b4784d4d-4dca-4b4a-8d46-bf7ebaa3fbfa" });
        }
    }
}
