using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedAplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AlterTable(
                name: "Products",
                oldComment: "Represents a product listing posted by a user for sale.");

            migrationBuilder.AlterTable(
                name: "Messages",
                oldComment: "Stores messages exchanged between users, typically related to a product.");

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                oldComment: "Extends the default ASP.NET Identity user with custom application-specific properties.");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c445414a-69cc-4deb-82d8-8495ad69932d", "AQAAAAIAAYagAAAAEE/noswfxVhUKWoNPQH8hYc+1mQpCZ8clJQ0/x/7dSSQNS7sjFlmHbvLL/lFslLg7Q==", "ef97fe2f-3ce4-4a5e-ba33-92b8dc9dfc83" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-3b4f-4f80-9b6e-4493d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "683c1e3a-83e5-4f51-8e2b-c3b87adcf255", "AQAAAAIAAYagAAAAEHdJU6FXYBrdYTsOvEb4Pfus+m45Q90SV+D/wX/Br8DJC4g9ovuNr/gyGOqJOfR9BA==", "2f1e8ae9-e813-4424-b18a-224fb0c7b304" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Products",
                comment: "Represents a product listing posted by a user for sale.");

            migrationBuilder.AlterTable(
                name: "Messages",
                comment: "Stores messages exchanged between users, typically related to a product.");

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                comment: "Extends the default ASP.NET Identity user with custom application-specific properties.");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "The city where the user is located.");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "The first name of the user.");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "The last name of the user.");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "City", "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "Sofia", "3782179a-c730-4e43-b163-094fef296752", "System", "Administrator", "AQAAAAIAAYagAAAAEOxqaDb9nPFfV6KRFo2lW/cpRyi0k/pYEjncAPCqFkmRIsxAYVrI9QjFqKltOD4VzA==", "797f49a4-8bcb-4d69-b697-a7c98bb6208d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-3b4f-4f80-9b6e-4493d048cdb0",
                columns: new[] { "City", "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "Plovdiv", "8ff65d19-9649-4482-826c-7d3b572a7c37", "Regular", "User", "AQAAAAIAAYagAAAAEKnasXjYPjw8/ajezwAaAVArpzXmGdp02/9XSr9bu0tT85FlH8dZl/6voBN7NtvbEA==", "bc85e24a-7685-4510-b6bd-cfa161cc72b9" });
        }
    }
}
